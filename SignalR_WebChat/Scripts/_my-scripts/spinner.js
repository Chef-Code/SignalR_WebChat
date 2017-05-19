//spinner.js
import {spinner} from 'spin';
function MySpinner(el) {
    let options = {
        lines: 8,
        speed: 2,
        radius: 3,
        length: 3,
        width: 2,
        top: -20,
        zIndex: 10,
        left: 35
    };

    let mySpinner = $('#activitySpinner').spinner(options);
    el.css('opacity','0.3');

};
