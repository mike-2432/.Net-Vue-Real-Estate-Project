# SSL Certificate for a secure HTTPS connection
if [ -f "nginx/certs/host.cert" ] && [ -f "nginx/certs/host.key" ]; 
then
    echo "SSL Certificate is already present."
else
    cd nginx
    mkdir certs
    sudo chmod ugo+w certs
    cd certs
    sudo openssl genrsa 2048 > host.key
    sudo chmod 400 host.key
    sudo openssl req -new -x509 -nodes -sha256 -days 365 -key host.key -out host.cert
    cd ../..
fi