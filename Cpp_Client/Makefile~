CXX=g++
TARGET=miku_cpp_client
SOURCE=miku_cpp_client.cpp
LIB=-lboost_system -lpthread
CXXFLAGS=-DNDEBUG -O2

miku_cpp_client:
	g++ -o $(TARGET) $(SOURCE) $(LIB) $(CXXFLAGS)
clean:
	rm $(TARGET)
