
\ You can read/write from save-file (external RAM)
\ only when its enabled.
\ Make sure to disable external RAM access when not reading/writing
\ or it may get corrupted.

: enable-external-ram
  $0a $0000 c! ;

: disable-external-ram
  $00 $0000 c! ;

