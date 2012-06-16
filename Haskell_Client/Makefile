#Haskell TCPSocket Client
HS=ghc
TARGETHS=MikuSocClient
SOURCEHS=MikuSocClient.hs
HSFLAGS=--make -O2
HSOBJECT=$(TARGETHS) $(TARGETHS).hi $(TARGETHS).o

$(TARGETHS):
	$(HS) $(HSFLAGS) $(SOURCEHS)
.PHONY: clean
clean:
	rm $(HSOBJECT)
