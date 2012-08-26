CC=mcs
CFLAGS=
MONO=mono
MFLAGS=

all: wordgame

wordgame: wordgame.cs
	$(CC) $(CFLAGS) wordgame.cs -out:wordgame.exe

clean:
	rm -f wordgame.exe

run: all
	$(MONO) $(MFLAGS) wordgame.exe

