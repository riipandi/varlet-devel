# Varlet Core

Inspired from Laravel Valet. Valet is a Laravel development environment for minimalists. Varlet Core is main
packages, it's only include PHP and Nginx. If you want to use a database like MariaDB, MySQL, PostgreSQL, etc,
you need to install it separately.

Varlet is made for you, the developers who like to work in the terminal, like me!

## What's in the box?

- PHP 7.3 + ImageMagick + ionCube Loader + PHP Redis extension
- PHP 7.2 + ImageMagick + ionCube Loader + PHP Redis extension
- PHP 5.6 + ImageMagick + ionCube Loader
- Latest version of Composer
- Nginx Mainline

## Quick Start

Download [latest release](https://github.com/riipandi/varlet-core/releases) then run the installation file.

Varlet doesn't have `park` command like Laravel Valet does. Your project files can stored at:
`installation_path\core\htdocs`. Or, you can use the `varlet link` command and place your project
files in any directory you want.

<!-- ## Varlet Commands

| Command                      | Description
| :--------------------------- | :----------
| varlet link                  | Create virtualhost and serving the site.
| varlet link-secure           | Create virtualhost and serving the site with https.
| varlet unlink                | Remove virtualhost.
| varlet unlink-secure         | Remove https virtualhost.
| varlet forget                | Remove both of virtualhost http and https.
| varlet start                 | Start Nginx + PHP-FPM services.
| varlet log                   | View a list of logs which are written by Varlet's services.
| varlet stop                  | Stop Nginx + PHP-FPM services.
| varlet restart               | Restart Nginx + PHP-FPM services.
| varlet status                | View site link status.
| varlet service-status        | View Nginx and PHP-FPM services status.
| varlet switch-php _version_  | Switch the default PHP version (version: 5.6/7.2/7.3). -->

## Things to know

### Additional Packages

- <https://github.com/riipandi/varlet-mariadb>
- <https://github.com/riipandi/varlet-mysql>
- <https://github.com/riipandi/varlet-postgresql>
<!-- - <https://github.com/riipandi/varlet-mongodb> -->
<!-- - <https://github.com/riipandi/varlet-redis> -->

### Useful Documentation

- <https://laravel.com/docs/valet>

## License

Varlet is free software: you can distribute it and or modify it according to the license provided.
Varlet is a compilation of free software, it's free of charge and it's free to copy under the terms
of the [Apache License 2.0](https://choosealicense.com/licenses/apache-2.0/). Please check every
single licence of the contained products to get an overview of what is, and what isn't, allowed.
In the case of commercial use please take a look at the product licences (_especially MySQL_),
from the my point of view commercial use is also free.

Read the [licence file](./license.txt) file for the full Varlet license text.
