#boost TCPSocket Client
CXX=g++
TARGET=miku_cpp_client
SOURCE=miku_cpp_client.cpp
LIB=-lboost_system -lpthread
CXXFLAGS=-DNDEBUG -O2
miku_cpp_client:
	$(CXX) -o $(TARGET) $(SOURCE) $(LIB) $(CXXFLAGS)
.PHONY: clean
clean:
	rm $(TARGET)
