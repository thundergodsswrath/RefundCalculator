﻿document.addEventListener("DOMContentLoaded", function() {
    isFullCourse();
});

function isFullCourse() {
    var minFullCoursePrice = 991;
    var number = document.getElementById('price-course').value;
    var radio1 = document.getElementById('radio1');
    var radio2 = document.getElementById('radio2');
    var radio3 = document.getElementById('radio3');

    if (number >= minFullCoursePrice) {
        radio1.disabled = false;
        radio2.disabled = false;
        radio3.disabled = false;
        if(radio3.checked === false && radio2.checked === false){
            radio1.checked = true;
        }
    } else {
        radio1.disabled = true;
        radio2.disabled = true;
        radio3.disabled = true;
        radio1.checked = false;
        radio2.checked = false;
        radio3.checked = false;
    }
}
