#include <string>
#include "table.h"
#include "intermediate.h"
#include <iostream>

using namespace std;

void initialize(EntryNode* node)
{
	node->entry = nullptr;
	node->prev = nullptr;
	node->next = nullptr;
}

int getattrindex(string attr, const string* attrs, int numAttrs)
{
	int j = 0;
	while (j < numAttrs)
	{
		if (attrs[j]._Equal(attr))
		{
			return j;
		}
		j++;
	}
	return -1;
}

Intermediate::Intermediate(const Table& Table)
{
	numAttrs = Table.numAttrs;
	attrs = Table.attrs;

	for (int i = 0; i < Table.numEntries; i++)
	{
		if (i == 0)
		{
			head = new EntryNode;
			initialize(head);
			head->entry = Table.entries[i];	
			if (i == Table.numEntries - 1)
			{
				tail = new EntryNode;
				initialize(tail);
				tail->entry = Table.entries[i];
			}
			continue;
		}

		EntryNode* last = head;
		while (last->next != nullptr)
		{
			last->next->prev = last;
			last = last->next;
		}

		last->next = new EntryNode;
		initialize(last->next);
		last->next->prev = last;
		last->next->entry = Table.entries[i];

		if (i == Table.numEntries - 1)
		{
			tail = new EntryNode;
			initialize(tail);
			tail->entry = Table.entries[i];
			tail->prev = last;
		}
	}	
}

Intermediate::~Intermediate()
{
	EntryNode* temp, *last = head;

	while (last != nullptr)
	{
		temp = last->next;
		delete last;
		last = temp;
	}
	delete tail;
}

Intermediate& Intermediate::where(const string& attr, compare mode, const string& value)
{
	int attrindex = getattrindex(attr, this->attrs, this->numAttrs);
	if (attrindex == -1) return *this;

	EntryNode* last = head;

	if (mode == EQ)
	{
		while (last != nullptr)
		{
			if (!last->entry[attrindex]._Equal(value))
			{
				EntryNode* del = last;
				last = last->next;
				if (head == nullptr || del == nullptr) continue;
				if (head == del) head = del->next;
				if (del->next != nullptr) del->next->prev = del->prev;
				if (del->prev != nullptr) del->prev->next = del->next;
				delete del;
				continue;
			}
			last = last->next;
		}
	}
	else
	{
		while (last != nullptr)
		{
			if (last->entry[attrindex].find(value) == string::npos)
			{
				EntryNode* del = last;
				last = last->next;
				if (head == nullptr || del == nullptr) continue;
				if (head == del) head = del->next;
				if (del->next != nullptr) del->next->prev = del->prev;
				if (del->prev != nullptr) del->prev->next = del->next;
				delete del;
				continue;
			}
			last = last->next;
		}
	}
	return *this;
}

Intermediate& Intermediate::orderBy(const string& attr, order order)
{
	int index = getattrindex(attr, this->attrs, this->numAttrs);
	if (index == -1) return *this;
	if (head == nullptr) return *this;

	int swapped;
	EntryNode* ptr1;
	EntryNode* lptr = nullptr;	
	if (order == ASCENDING)
	{
		do
		{
			swapped = 0;
			ptr1 = head;
			while (ptr1->next != lptr)
			{
				if (ptr1->entry[index].compare(ptr1->next->entry[index]))
				{
					swap(ptr1->entry, ptr1->next->entry);
					swapped = 1;
				}
				ptr1 = ptr1->next;
			}
			lptr = ptr1;
		} while (swapped);
	}
	else
	{
		do
		{
			swapped = 0;
			ptr1 = head;
			while (ptr1->next != lptr)
			{
				if (ptr1->next->entry[index].compare(ptr1->entry[index]))
				{
					swap(ptr1->entry, ptr1->next->entry);
					swapped = 1;
				}
				ptr1 = ptr1->next;
			}
			lptr = ptr1;
		} while (swapped);

	}
	return *this;
}

Intermediate& Intermediate::limit(unsigned int limit)
{
	EntryNode* last = head;
	bool outbound = true;

	int i = 1;
	while (last != nullptr)
	{
		if (i == limit)
		{
			outbound = false;
			break;
		}
		last = last->next;
		i++;
	}
	if (outbound && limit > 0) return *this;

	EntryNode* temp, * first = tail->prev->next;

	while (first != last && first != nullptr)
	{
		temp = first->prev;
		delete first;
		first = temp;
	}

	delete tail;
	tail = new EntryNode;
	initialize(tail);
	if (last != nullptr)
	{
		last->next = nullptr;
		tail->prev = last->prev;		
	}

	if (limit == 0) head = nullptr;
	return *this;
}

void Intermediate::select(const string* attrs, int numAttrs) const
{
	numAttrs = attrs == nullptr ? this->numAttrs : numAttrs;
	attrs = attrs == nullptr ? this->attrs : attrs;

	unsigned long* widths = new unsigned long[this->numAttrs];
	for (int i = 0; i < this->numAttrs; i++)
	{
		widths[i] = this->attrs[i].length();
	}

	EntryNode* last = head;
	while (last != nullptr)
	{
		for (int i = 0; i < this->numAttrs; i++)
		{
			widths[i] = last->entry[i].length() > widths[i] ? last->entry[i].length() : widths[i];
		}
		last = last->next;
	}

	unsigned int* attrsindexes = new unsigned int[numAttrs];
	for (int i = 0; i < numAttrs; i++)
	{
		attrsindexes[i] = getattrindex(attrs[i], this->attrs, this->numAttrs);
	}

	for (int i = 0; i < numAttrs; i++)
	{
		cout << _left_pad_until(attrs[i], widths[attrsindexes[i]]) << " | ";
	}
	cout << endl;
	
	last = head;
	while (last != nullptr)
	{
		for (int j = 0; j < numAttrs; j++)
		{
			cout << _left_pad_until(last->entry[attrsindexes[j]], widths[attrsindexes[j]]) << " | ";
		}
		cout << endl;
		last = last->next;
	}

	delete[] attrsindexes;
	delete[] widths;
}

void Intermediate::update(const string& attr, const string& new_value) const
{
	int attrindex = getattrindex(attr, this->attrs, this->numAttrs);
	if (attrindex == -1) return;

	EntryNode* last = head;
	while (last != nullptr)
	{
		last->entry[attrindex] = new_value;
		last = last->next;
	}
}
