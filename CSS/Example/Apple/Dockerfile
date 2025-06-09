FROM busybox:latest
COPY Apple.html /www/index.html
COPY font/ /www/font/
COPY image/ /www/image/
COPY css/ /www/css/
CMD ["httpd", "-f", "-p", "80", "-h", "/www"]