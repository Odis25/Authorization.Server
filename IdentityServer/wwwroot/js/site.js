// Данная идея и ее реализация позаимствована с сайта http://www.panthers.ru/tamer/login/
// за что им огромная благодарность!
(function () {

    var inputLogin = document.querySelector('#input-login');
    var inputPassword = document.querySelector('#input-password');

    var eyeFirstLeft = document.querySelector('.animation__eye--one-one');
    var eyeFirstRight = document.querySelector('.animation__eye--one-two');

    var eyeSecondLeft = document.querySelector('.animation__eye--two-one');
    var eyeSecondRight = document.querySelector('.animation__eye--two-two');

    inputLogin.addEventListener('focus', firstFocusHandler);
    inputPassword.addEventListener('focus', firstFocusHandler);

    inputLogin.addEventListener('input', inputHandler);
    inputPassword.addEventListener('input', inputHandler);

    inputLogin.addEventListener('blur', blurHandler);
    inputPassword.addEventListener('blur', blurHandler);

    function firstFocusHandler(evt) {
        var animation = document.querySelector('.animation');
        var animationVideo = document.querySelector('.animation__video');
        var eye = document.querySelectorAll('.animation__eye');
        var button = document.querySelector('.btn');

        for (var i = 0; i < eye.length; i++) {
            eye[i].style.display = 'block';
            fadeIn(eye[i], 2000);
            focusHandler(evt);
        }

        animation.style.maxHeight = '374px';
        animationVideo.style.display = 'block';
        fadeIn(animationVideo, 2000);
        animationVideo.play();

        inputLogin.removeEventListener('focus', firstFocusHandler);
        inputPassword.removeEventListener('focus', firstFocusHandler);

        inputLogin.addEventListener('focus', focusHandler);
        inputPassword.addEventListener('focus', focusHandler);

        button.addEventListener('click', clickHandler);
    }

    function fadeIn(elem, speed) {
        var inInterval = setInterval(function () {
            elem.style.opacity = Number(elem.style.opacity) + 0.02;
            if (elem.style.opacity >= 1)
                clearInterval(inInterval);
        }, speed / 50);
    }

    function fadeOut(elem, speed) {
        var outInterval = setInterval(function () {
            if (!elem.style.opacity) {
                elem.style.opacity = 1;
            }
            elem.style.opacity -= 0.02;
            if (elem.style.opacity <= 0)
                clearInterval(outInterval);
        }, speed / 50);
    }

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
        if (len < 2) {
            setPosition('First', 58, 112, 103, 113);
        }
        else if (len < 3) {
            setPosition('First', 59, 112, 104, 113);
        }
        else if (len < 4) {
            setPosition('First', 60, 112, 105, 113);
        }
        else if (len < 5) {
            setPosition('First', 61, 112, 106, 113);
        }
        else if (len < 6) {
            setPosition('First', 62, 112, 106, 113);
        }
        else if (len < 7) {
            setPosition('First', 63, 112, 107, 113);
        }
        else if (len < 8) {
            setPosition('First', 64, 112, 108, 113);
        }
        else if (len < 9) {
            setPosition('First', 65, 112, 109, 113);
        }
        else if (len > 12) {
            setPosition('First', 66, 112, 110, 113);
        }
    }

    function setPositionTwo(len) {
        if (len < 2) {
            setPosition('Second', 247, 169, 292, 170);
        }
        else if (len < 3) {
            setPosition('Second', 248, 170, 293, 171);
        }
        else if (len < 4) {
            setPosition('Second', 249, 170, 294, 171);
        }
        else if (len < 5) {
            setPosition('Second', 250, 170, 295, 171);
        }
        else if (len < 6) {
            setPosition('Second', 251, 170, 296, 171);
        }
        else if (len < 7) {
            setPosition('Second', 252, 170, 296, 171);
        }
        else if (len < 8) {
            setPosition('Second', 252, 170, 296, 171);
        }
        else if (len === 11) {
            setPosition('Second', 253, 170, 297, 171);
        }
        else if (len === 12) {
            setPosition('Second', 254, 170, 298, 171);
        }
        else if (len === 13) {
            setPosition('Second', 255, 170, 299, 171);
        }
        else if (len > 13) {
            setPosition('Second', 256, 170, 300, 171);
        }
    }

    function blurHandler() {
        setPosition('First', 60, 109, 106, 110);
        setPosition('Second', 252, 166, 298, 166);
    }

    function clickHandler() {
        var wrapper = document.querySelector('.wrapper');
        inputPassword.type = 'text';
        fadeOut(wrapper, 1000);
    }

    function setPosition(person, xLeft, yLeft, xRight, yRight) {
        eval('eye' + person + 'Left').style.left = xLeft + 'px';
        eval('eye' + person + 'Left').style.top = yLeft + 'px';
        eval('eye' + person + 'Right').style.left = xRight + 'px';
        eval('eye' + person + 'Right').style.top = yRight + 'px';
    }
})();