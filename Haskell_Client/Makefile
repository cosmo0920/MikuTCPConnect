#Haskell TCPSocket Client
HS=ghc
TARGETHS=MikuSocClient
SOURCEHS=MikuSocClient.hs
HSFLAGS=--make -O2
HSOBJECT=$(TARGETHS) $(TARGETHS).hi $(TARGETHS).o

$(TARGET):
	$(HS) $(HFLAGS) $(SOURCE)
.PHONY: clean
clean:
	rm $(HSOBJECT)
