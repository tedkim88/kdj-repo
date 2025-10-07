# PHP + Apache 베이스 이미지
FROM php:8.4-apache

# 필요한 OS 패키지와 PHP 확장 설치
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
      curl \
      zip unzip \
      git \
      libfreetype6-dev \
      libjpeg62-turbo-dev \
      libpng-dev \
      libwebp-dev \
      libavif-dev \
      libaom-dev \
      libdav1d-dev \
      libyuv-dev \
    && docker-php-ext-configure gd \
      --with-freetype=/usr/include/ \
      --with-jpeg=/usr/include/ \
      --with-webp \
      --with-avif \
    && docker-php-ext-install -j"$(nproc)" \
      gd \
      pdo_mysql \
      mysqli \
    && rm -rf /var/lib/apt/lists/*

# Apache URL 재작성 활성화
RUN a2enmod rewrite

# Create upload directories and fix permissions
RUN chown -R www-data:www-data /var/www/html/public/images \
    && chmod -R 755 /var/www/html/public/images

# 커스텀 Apache 설정 적용 (디렉토리 리스팅, DocumentRoot 등)
COPY apache-dirlist.conf /etc/apache2/sites-enabled/000-default.conf

# 프로젝트 소스 복사
# - public 폴더: 웹에서 접근 가능
# - private 폴더: PHP 코드
# - data 폴더: 외부에서 접근 불가
COPY public/ /var/www/html/public/
COPY private/ /var/www/html/private/
COPY data/ /var/www/html/data/

# 웹 서비스 포트 (Render 기본 10000)
EXPOSE 10000

# Apache 포그라운드 실행
CMD ["apache2-foreground"]

