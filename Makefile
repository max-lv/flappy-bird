
GBFORTH_ROOT = "$(abspath $(shell pwd)/../gbforth)"

%.gb: %.fs
	GBFORTH_PATH=$(GBFORTH_ROOT)"/lib/" \
	  $(GBFORTH_ROOT)/gbforth $<

.PHONY: test
test:
	echo "hello" $(GBFORTH_ROOT)

all: flappy-bird.gb
	echo "done"

