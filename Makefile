
GBFORTH_ROOT = "$(abspath $(shell pwd)/../gbforth)"

flappy-bird.gb: *.fs
	GBFORTH_PATH=$(GBFORTH_ROOT)"/lib/" \
	  $(GBFORTH_ROOT)/gbforth flappy-bird.fs

all: flappy-bird.gb
	echo "done"

