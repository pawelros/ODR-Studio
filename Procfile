dabplus-enc: dabplus-enc -v http://192.168.1.2:8081 -r 48000 -c 2 -o tcp://0.0.0.0:60001 -l -b 128 -P /home/pr/odr_studio/data/mot_slideshow -p 58
dabmux: odr-dabmux -e /home/pr/odr_studio/src/dabmux.config
ETI-player: dablin -s 0x0033 < data/pipe
WEB-API: cd ~/odr_studio/src/OdrStudio.WebApi && dnx web
WEB-GUI: cd ~/odr_studio/src/OdrStudio.WebApp && dnx web