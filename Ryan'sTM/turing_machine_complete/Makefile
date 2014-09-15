.PHONY: all clean

cc=g++
OBJDIR=objects
CFLAGS=-g
extras=Direction.h
obj= ParseClass.o InputStringClass.o CommandClass.o ConfigurationClass.o Crash.o DisplayClass.o FinalStatesClass.o InputAlphabetClass.o StatesClass.o TapeAlphabetClass.o TapeClass.o TransitionClass.o TuringMachineClass.o TransitionFunctionClass.o 

executable=turingMachine

all: $(executable)

$(executable): $(obj) $(extras) main.cpp
	$(cc) $(CFLAGS) -o $@ main.cpp $(obj)
%.o: %.cpp %.h
	$(cc) $(CFLAGS) -c $< -o $@
clean:
	rm $(executable) $(obj)
