while [ 1 ];
  do 
    vardate=$(date +%d\-%m\-%Y\_%H.%M.%S);
    screencapture -t jpg -x ~/Desktop/screen/pics/$vardate.jpg;
    sleep 30;
  done
