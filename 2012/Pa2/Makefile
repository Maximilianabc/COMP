

ifeq (Windows_NT, $(OS))

lib       := ncurses_lib/pdcurses
flag      := -D_WIN_  
suf       := pdcurses.dll
linkflag  := -l$(lib)  -static -L".\\"
ifeq (, $(OSTYPE))
ziptool   := zip.exe
else
ziptool   := zip
endif
else
linkflag  := 
lib       := ncurses
flag      := -D_MAC_
ziptool   := zip
suf       := -l$(lib)
endif


TARGET    := pa2.exe pa2_test.exe
CFLAGS    := -Wall -std=c++11 $(flag) 
OBJDIR    := object
BASE      := Controller View Object TodoEmployee TodoProperty main
TEST      := Controller View Object TodoEmployee TodoProperty test
TODO      := TodoFarmer TodoFarmland TodoFeeder TodoCow TodoCattlefarm TodoScene
BASE_OBJ  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(BASE)))
TODO_OBJ  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TODO)))
TEST_OBJ  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST)))

all: build pa2.zip

build: create_directory $(TARGET)

create_directory: object 

object:
	@mkdir -p $(OBJDIR) 

pa2.exe: $(TODO_OBJ) $(BASE_OBJ)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(BASE_OBJ) $(suf)

pa2_test.exe: $(TODO_OBJ) $(TEST_OBJ)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ) $(suf)

object/%.o: %.cpp 
	g++ $(CFLAGS) -c -o $@ $<

clean:
	rm -f ./object/* pa2.exe pa2_test.exe pa2.zip

pa2.zip: $(addsuffix .cpp, $(TODO)) TodoFarmer.h TodoFeeder.h TodoCow.h TodoFarmland.h TodoCattlefarm.h
	@echo
	@echo "Preparing your pa2.zip - submit this to Canvas directly"
	@echo
	rm -f pa2.zip
	$(ziptool) pa2.zip Todo*