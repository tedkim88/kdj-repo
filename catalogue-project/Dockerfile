# Base image
FROM php:8.4-apache

# Install required OS packages and PHP extensions
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

# Enable Apache modules
RUN a2enmod rewrite

# Copy Apache config (for directory listing, if needed)
COPY apache-dirlist.conf /etc/apache2/sites-enabled/000-default.conf

# Create upload directories and fix permissions
RUN chown -R www-data:www-data /var/www/html/public/images \
    && chmod -R 755 /var/www/html/public/images

# Copy your project into container
COPY ./catalogue-project /var/www/html/

# Set ServerName to suppress Apache warnings
RUN echo "ServerName localhost" >> /etc/apache2/apache2.conf

# Expose default HTTP port
EXPOSE 80

# Start Apache in foreground
CMD ["apache2-foreground"]
