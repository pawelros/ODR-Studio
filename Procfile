dabplus-enc: dd if=/home/pr/odr_studio/data/mot_slideshow iflag=nonblock of=/dev/nul;dabplus-enc -v http://192.168.1.2:8081 -r 48000 -c 2 -o tcp://0.0.0.0:60001 -l -b 128 -P /home/pr/odr_studio/data/mot_slideshow -p 58
dabmux: odr-dabmux -e /home/pr/odr_studio/src/dabmux.config
mot-encoder: mot-encoder -d "/mnt/hgfs/artistalbum/TestSlideShow/" -o /home/pr/odr_studio/data/mot_slideshow -t "/mnt/hgfs/artistalbum/TestDls/dls.txt" -v
ETI-player: dablin_gtk -s 0x0033 < data/pipe