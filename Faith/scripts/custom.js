

var isProgress = false;
var timer = "";
function progressShow() {
    if (!isProgress) {
        $('.progress-bg').show();
        var progressInt = $('.progress-blue').css('width').replace('px', '');
        timer = setInterval(function () {
            progressInt += 1;
            if (progressInt>100) {
                progressInt = 0;
            }
            $('.progress-blue').css('width', progressInt+'px');
        }, 10);
        isProgress = true;
    }
}
function progressClose() {
    clearInterval(timer);
    isProgress = false;
    $('.progress-bg').hide();
}