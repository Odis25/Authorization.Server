// Данная идея и ее реализация позаимствована с сайта http://www.panthers.ru/tamer/login/
// за что им огромная благодарность!
(function () {

    var inputLogin = document.querySelector('#input-login');
    var inputPassword = document.querySelector('#input-password');

    var eyeFirstLeft = document.querySelector('.animation__eye--one-one');
    var eyeFirstRight = document.querySelector('.animation__eye--one-two');

    var eyeSecondLeft = document.querySelector('.animation__eye--two-one');
    var eyeSecondRight = document.querySelector('.animation__eye--two-two');

    inputLogin.addEventListener('focus', focusHandler);
    inputPassword.addEventListener('focus', focusHandler);

    inputLogin.addEventListener('input', inputHandler);
    inputPassword.addEventListener('input', inputHandler);

    inputLogin.addEventListener('blur', blurHandler);
    inputPassword.addEventListener('blur', blurHandler);

    blurHandler();

    function focusHandler(evt) {
        evt.preventDefault();
        evt.stopPropagation();
        var length = evt.target.value.length;
        setPositionOne(length);
        setPositionTwo(length);
    }

    function inputHandler(evt) {
        var length = evt.target.value.length;
        setPositionOne(length);
        setPositionTwo(length);
    }

    function setPositionOne(len) {
        if (len < 3) {
            setPosition('First', 58, 112, 103, 113);
        }
        else if (len < 5) {
            setPosition('First', 59, 112, 104, 113);
        }
        else if (len < 7) {
            setPosition('First', 60, 112, 105, 113);
        }
        else if (len < 9) {
            setPosition('First', 61, 112, 106, 113);
        }
        else if (len < 11) {
            setPosition('First', 62, 112, 107, 113);
        }
        else if (len < 13) {
            setPosition('First', 63, 112, 108, 113);
        }
        else if (len < 15) {
            setPosition('First', 64, 112, 109, 113);
        }
        else if (len < 17) {
            setPosition('First', 65, 112, 110, 113);
        }
        else if (len > 19) {
            setPosition('First', 66, 112, 111, 113);
        }
    }

    function setPositionTwo(len) {
        if (len < 3) {
            setPosition('Second', 247, 169, 292, 170);
        }
        else if (len < 6) {
            setPosition('Second', 248, 170, 293, 171);
        }
        else if (len < 9) {
            setPosition('Second', 249, 170, 294, 171);
        }
        else if (len < 12) {
            setPosition('Second', 250, 170, 295, 171);
        }
        else if (len < 15) {
            setPosition('Second', 251, 170, 296, 171);
        }
        else if (len < 18) {
            setPosition('Second', 252, 170, 296, 171);
        }
        else if (len < 20) {
            setPosition('Second', 252, 170, 296, 171);
        }
        else if (len === 30) {
            setPosition('Second', 253, 170, 297, 171);
        }
        else if (len === 32) {
            setPosition('Second', 254, 170, 298, 171);
        }
        else if (len === 34) {
            setPosition('Second', 255, 170, 299, 171);
        }
        else if (len > 35) {
            setPosition('Second', 256, 170, 300, 171);
        }
    }

    function blurHandler() {
        setPosition('First', 60, 109, 106, 110);
        setPosition('Second', 252, 166, 298, 166);
    }

    function setPosition(person, xLeft, yLeft, xRight, yRight) {
        eval('eye' + person + 'Left').style.left = xLeft + 'px';
        eval('eye' + person + 'Left').style.top = yLeft + 'px';
        eval('eye' + person + 'Right').style.left = xRight + 'px';
        eval('eye' + person + 'Right').style.top = yRight + 'px';
    }
})();