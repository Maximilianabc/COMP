

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

TEST0      := Controller View Object TodoEmployee TodoProperty test_case0
TEST_OBJ0  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST0)))
TEST1      := Controller View Object TodoEmployee TodoProperty test_case1
TEST_OBJ1  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST1)))
TEST2      := Controller View Object TodoEmployee TodoProperty test_case2
TEST_OBJ2  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST2)))
TEST3      := Controller View Object TodoEmployee TodoProperty test_case3
TEST_OBJ3  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST3)))
TEST4      := Controller View Object TodoEmployee TodoProperty test_case4
TEST_OBJ4  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST4)))
TEST5      := Controller View Object TodoEmployee TodoProperty test_case5
TEST_OBJ5  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST5)))
TEST6      := Controller View Object TodoEmployee TodoProperty test_case6
TEST_OBJ6  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST6)))
TEST7      := Controller View Object TodoEmployee TodoProperty test_case7
TEST_OBJ7  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST7)))
TEST8      := Controller View Object TodoEmployee TodoProperty test_case8
TEST_OBJ8  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST8)))
TEST9      := Controller View Object TodoEmployee TodoProperty test_case9
TEST_OBJ9  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST9)))
TEST10      := Controller View Object TodoEmployee TodoProperty test_case10
TEST_OBJ10  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST10)))
TEST11      := Controller View Object TodoEmployee TodoProperty test_case11
TEST_OBJ11  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST11)))
TEST12      := Controller View Object TodoEmployee TodoProperty test_case12
TEST_OBJ12  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST12)))
TEST13      := Controller View Object TodoEmployee TodoProperty test_case13
TEST_OBJ13  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST13)))
TEST14      := Controller View Object TodoEmployee TodoProperty test_case14
TEST_OBJ14  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST14)))
TEST15      := Controller View Object TodoEmployee TodoProperty test_case15
TEST_OBJ15  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST15)))
TEST16      := Controller View Object TodoEmployee TodoProperty test_case16
TEST_OBJ16  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST16)))
TEST17      := Controller View Object TodoEmployee TodoProperty test_case17
TEST_OBJ17  := $(addsuffix .o, $(addprefix $(OBJDIR)/, $(TEST17)))

check_compilation: build

grade_clean: 
	rm -f ./object/* *.exe *.zip *.log

check: check_compilation check_case0 check_case1 check_case2 check_case3 check_case4 check_case5 check_case6 check_case7 check_case8 check_case9 check_case10 check_case11 check_case12 check_case13 check_case14 check_case15 check_case16 check_case17

check_case0: $(TODO_OBJ) $(TEST_OBJ0)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ0) $(suf)

check_case1: $(TODO_OBJ) $(TEST_OBJ1)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ1) $(suf)

check_case2: $(TODO_OBJ) $(TEST_OBJ2)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ2) $(suf)

check_case3: $(TODO_OBJ) $(TEST_OBJ3)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ3) $(suf)

check_case4: $(TODO_OBJ) $(TEST_OBJ4)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ4) $(suf)

check_case5: $(TODO_OBJ) $(TEST_OBJ5)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ5) $(suf)

check_case6: $(TODO_OBJ) $(TEST_OBJ6)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ6) $(suf)

check_case7: $(TODO_OBJ) $(TEST_OBJ7)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ7) $(suf)

check_case8: $(TODO_OBJ) $(TEST_OBJ8)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ8) $(suf)

check_case9: $(TODO_OBJ) $(TEST_OBJ9)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ9) $(suf)

check_case10: $(TODO_OBJ) $(TEST_OBJ10)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ10) $(suf)

check_case11: $(TODO_OBJ) $(TEST_OBJ11)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ11) $(suf)

check_case12: $(TODO_OBJ) $(TEST_OBJ12)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ12) $(suf)

check_case13: $(TODO_OBJ) $(TEST_OBJ13)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ13) $(suf)
	
check_case14: $(TODO_OBJ) $(TEST_OBJ14)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ14) $(suf)

check_case15: $(TODO_OBJ) $(TEST_OBJ15)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ15) $(suf)

check_case16: $(TODO_OBJ) $(TEST_OBJ16)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ16) $(suf)

check_case17: $(TODO_OBJ) $(TEST_OBJ17)
	g++ $(CFLAGS) $(linkflag) -o $@ $(TODO_OBJ) $(TEST_OBJ17) $(suf)