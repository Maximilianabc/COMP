#include <string>
#include "table.h"
#include <iostream>
using namespace std;

Table::Table()
{
	attrs = nullptr;
	entries = nullptr;
	numAttrs = 0;
	numEntries = 0;
}

Table::Table(const Table& another)
{
	numAttrs = another.numAttrs;
	numEntries = another.numEntries;
	if (another.attrs)
	{
		attrs = new string[numAttrs];
		for (int i = 0; i < numAttrs; i++)
		{
			attrs[i] = another.attrs[i];
		}
	}
	else
	{
		attrs = nullptr;
	}
	if (another.entries)
	{
		entries = new string * [numEntries];
		for (int i = 0; i < numEntries; i++)
		{
			entries[i] = new string[numAttrs];
			for (int j = 0; j < numAttrs; j++)
			{
				entries[i][j] = another.entries[i][j];
			}
		}
	}
	else
	{
		entries = nullptr;
	}
}

Table::~Table()
{
	delete[] attrs;
	for (int i = 0; i < numEntries; i++)
	{
		delete[] entries[i];
	}
	delete[] entries;
}

bool Table::addAttribute(const string& attr, int index, const string& default_value)
{
	if (index < -1 || index > numAttrs) return false;
	index = index == -1 ? numAttrs : index;
	++numAttrs;

	attrs = attrs == nullptr ? new string[numAttrs] : attrs;
	string* newattrs = new string[numAttrs];
	for (int i = numAttrs - 1; i > index; i--)
	{
		newattrs[i] = attrs[i - 1];
	}	
	newattrs[index] = attr;
	for (int i = 0; i < index; i++)
	{
		newattrs[i] = attrs[i];
	}
	
	delete[] attrs;
	attrs = new string[numAttrs];
	copy(newattrs, newattrs + numAttrs, attrs);

	if (numEntries > 0)
	{
		for (int i = 0; i < numEntries; i++)
		{
			string* newentry = new string[numAttrs];
			for (int j = numAttrs - 1; j > index; j--)
			{
				newentry[j] = entries[i][j - 1];
			}			
			newentry[index] = default_value;
			for (int k = 0; k < index; k++)
			{
				newentry[k] = entries[i][k];
			}	

			delete[] entries[i];
			entries[i] = new string[numAttrs];
			copy(newentry, newentry + numAttrs, entries[i]);
			delete[] newentry;
		}
	}
	delete[] newattrs;
	return true;
}

bool Table::addEntry(const string* entry, int index)
{
	if (index < -1 || index > numEntries) return false;
	index = index == -1 ? numEntries : index;
	++numEntries;

	if (entries == nullptr)
	{
		entries = new string * [numEntries];
		for (int i = 0; i < numEntries; i++)
		{
			entries[i] = new string[numAttrs];
		}
	}

	string** newentries = new string* [numEntries];
	for (int i = 0; i < numEntries; i++)
	{
		newentries[i] = new string[numAttrs];
	}

	for (int i = numEntries - 1; i > index; i--)
	{
		for (int j = 0; j < numAttrs; j++)
		{
			newentries[i][j] = i == 0 ? entry[j] : entries[i - 1][j];
		}	
	}
	for (int j = 0; j < numAttrs; j++)
	{
		newentries[index][j] = entry[j];
		for (int i = 0; i < index; i++)
		{
			newentries[i][j] = entries[i][j];
		}
	}
	
	int numdel = numEntries == 1 ? 1 : numEntries - 1;
	for (int i = 0; i < numdel; i++)
	{
		delete[] entries[i];
	}
	delete[] entries;

	entries = new string * [numEntries];
	for (int i = 0; i < numEntries; i++)
	{
		entries[i] = new string[numAttrs];
		for (int j = 0; j < numAttrs; j++)
		{
			entries[i][j] = newentries[i][j];
		}
	}

	for (int i = 0; i < numEntries; i++)
	{
		delete[] newentries[i];
	}
	delete[] newentries;
	return true;
}

bool Table::deleteAttribute(int index)
{
	if (index < 0 || index >= numAttrs) return false;
	--numAttrs;

	string* newattrs = new string[numAttrs];
	if (index != numAttrs)
	{
		for (int i = index; i < numAttrs; i++)
		{
			newattrs[i] = attrs[i + 1];
		}
	}
	for (int i = 0; i < index; i++)
	{
		newattrs[i] = attrs[i];
	}

	delete[] attrs;
	attrs = new string[numAttrs];
	copy(newattrs, newattrs + numAttrs, attrs);

	if (numEntries > 0)
	{
		for (int i = 0; i < numEntries; i++)
		{
			string* newentry = new string[numAttrs];
			for (int i = index; i < numAttrs; i++)
			{
				if (index != numAttrs)
				{
					for (int j = index; j < numAttrs; j++)
					{
						newentry[j] = entries[i][j + 1];
					}
				}
				for (int k = 0; k < index; k++)
				{
					newentry[k] = entries[i][k];
				}
			}

			delete[] entries[i];
			entries[i] = new string[numAttrs];
			copy(newentry, newentry + numAttrs, entries[i]);
			delete[] newentry;
		}
	}
	else
	{
		for (int i = 0; i < numEntries; i++)
		{
			delete[] entries[i];
		}
		delete[] entries;
	}
	delete[] newattrs;
	return true;
}

bool Table::deleteEntry(int index)
{
	if (index < 0 || index >= numEntries) return false;
	--numEntries;

	string** newentries = new string * [numEntries];
	for (int i = 0; i < numEntries; i++)
	{
		newentries[i] = new string[numAttrs];
	}

	if (index != numEntries)
	{
		for (int i = index; i < numEntries; i++)
		{
			for (int j = 0; j < numAttrs; j++)
			{
				newentries[i][j] = entries[i + 1][j];
			}
		}
	}
	for (int i = 0; i < index; i++)
	{
		for (int j = 0; j < numAttrs; j++)
		{
			newentries[i][j] = entries[i][j];
		}
	}

	for (int i = 0; i < numEntries + 1; i++)
	{
		delete[] entries[i];
	}
	delete[] entries;

	entries = new string* [numEntries];
	for (int i = 0; i < numEntries; i++)
	{
		entries[i] = new string[numAttrs];
		copy(newentries[i], newentries[i] + numAttrs, entries[i]);
	}

	for (int i = 0; i < numEntries; i++)
	{
		delete[] newentries[i];
	}
	delete[] newentries;
	return true;
}

bool Table::append(const Table& another)
{
	if (numAttrs != another.numAttrs) return false;
	for (int i = 0; i < numAttrs; i++)
	{
		if (!attrs[i]._Equal(another.attrs[i])) return false;
	}
	for (int i = 0; i < another.numEntries; i++)
	{
		this->addEntry(another.entries[i], -1);
	}
	return true;
}

// Not required, will not be graded
// For the purpose of debugging, you can implement this function to print out the table
void Table::print() const
{
	for (int j = 0; j < numAttrs; j++)
	{
		cout << attrs[j] << " ";
	}
	cout << endl;

	for (int i = 0; i < numEntries; i++)
	{
		for (int j = 0; j < numAttrs; j++)
		{
			cout << entries[i][j] << " ";
		}
		cout << endl;
	}
}
