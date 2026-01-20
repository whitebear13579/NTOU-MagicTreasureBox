// Fig. 12.5: coverviewer.js 
// Script to demonstrate dynamic styles used for animation. 
let interval = null; // keeps track of the interval
let speed = 6; // determines the speed of the animation
let count = 0; // size of the image during the animation

// called repeatedly to animate the book cover
function run() {
   count += speed;

   // stop the animation when the image is large enough
   if (count >= 375) {
      window.clearInterval(interval);
      interval = null;
   } // end if

   let bigImage = document.getElementById("imgCover");
   bigImage.setAttribute("style", "width: " + (0.7656 * count + "px;") +
      "height: " + (count + "px;"));
} // end function run

// inserts the proper image into the main image area and
// begins the animation
function display(imgfile) {
   if (interval)
      return;

   let bigImage = document.getElementById("imgCover");
   bigImage.setAttribute("style", "width: 0px; height: 0px;");
   bigImage.setAttribute("src", "fullsize/" + imgfile);
   bigImage.setAttribute("alt", "Large version of " + imgfile);
   count = 0; // start the image at size 0
   interval = window.setInterval("run()", 10); // animate
   // 每10毫秒调用一次run函数
} // end function display

// register event handlers
function start() {
   document.getElementById("jhtp").addEventListener(
      "click", function () { display("jhtp.jpg"); }, false);
   document.getElementById("iw3htp").addEventListener(
      "click", function () { display("iw3htp.jpg"); }, false);
   document.getElementById("cpphtp").addEventListener(
      "click", function () { display("cpphtp.jpg"); }, false);
   document.getElementById("jhtplov").addEventListener(
      "click", function () { display("jhtplov.jpg"); }, false);
   document.getElementById("cpphtplov").addEventListener(
      "click", function () { display("cpphtplov.jpg"); }, false);
   document.getElementById("vcsharphtp").addEventListener(
      "click", function () { display("vcsharphtp.jpg"); }, false);
} // end function start

window.addEventListener("load", start, false);

/*************************************************************************
* (C) Copyright 1992-2012 by Deitel & Associates, Inc. and               *
* Pearson Education, Inc. All Rights Reserved.                           *
*                                                                        *
* DISCLAIMER: The authors and publisher of this book have used their     *
* best efforts in preparing the book. These efforts include the          *
* development, research, and testing of the theories and programs        *
* to determine their effectiveness. The authors and publisher make       *
* no warranty of any kind, expressed or implied, with regard to these    *
* programs or to the documentation contained in these books. The authors *
* and publisher shall not be liable in any event for incidental or       *
* consequential damages in connection with, or arising out of, the       *
* furnishing, performance, or use of these programs.                     *
*************************************************************************/