
Technologies
============

* HTML 5.0

* CoffeeScript

* Less

3th Party Libs
==============

* jQuery 1.10

* Bootstrap (pendent to include)


Script & Style Compilation
==========================

Since CoffeeScript and Less requires compilation there is a brief about how to compile these files. The
documentation of these software is oriented to Ubuntu OS but of course you can find the same software
for almost any software-engineer friendly OS, in that case feel free to extend the documentation with your
knowledge.

nodejs
-------

[nodejs](http://nodejs.org/) is a platform to manage applications and packages. It is very powerful tool
to develop web applications but there is only used to download required software. You can install it
downloading the code from [nodejs.org/downloads](http://nodejs.org/download/). Some people feel more comfortable
installing software from package managers if available, if you match that premise then read
[this link](https://github.com/joyent/node/wiki/Installing-Node.js-via-package-manager).

CoffeeScript
------------

[CoffeeScript](http://coffeescript.org/) is a language that compiles (via translation) to javascript files.
It has very desirable features inspired in other high-level languages such as Python. Since it is compiled to
javascript there is not compatibility hazards.

It requires compiler installation. Install it via npm repositories:

    sudo npm install -g coffee-script

Once you have it installed `.coffee` files can be compiled to `.js` files executing:


    coffee --compile --output [outputDir] [sourcesDir]

Also you can run (and update) the shell file `compileCoffee.sh` that compiles all `.coffee` scripts.


Less
----

[Less](http://lesscss.org/) is a language that compiles (also via translation) to css. Among its features
it includes nested rules, variables, mixins (like parametrized classes) and function/operations.

It requires compiler installation. Install it via npm repositories:

    sudo npm install -g less

Once you have it intsalled `.less` files can be compiled to `.css` files executing:

    lessc [sourceLessFile] [outputCssFile]

Also you can run (and update) the shell file `compileLess.sh` that compiles all `.less` scripts.