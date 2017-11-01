const sendNum = (x, y, value) => {
    $.ajax({
        url: "/Home/SetValue/?posx=" + x + "&posy=" + y + "&value=" + value,
        type: "GET",
        error: function (response) {
            alert("500 server not found");
        },
    });
}

const sendBox = (x, y, value) => {
    console.log('id')
    console.log(x +' : '+ y)
    console.log('val')
    console.log(value)

    if (value === 0) {
        return;
    }
    //check if the inserted length is equal, if so reset and return
    if (value.length === 0) {
        //txtInput.val(0);
        return;
    }
    if (isNaN(value)) {
        //txtInput.val(0);
        $('.box-' + x + '-' + y).val(0)
        alert('no value');
        return;
    }
    if (value < 1 || value > 9) {
        //txtInput.val(0);
        $('.box-' + x + '-' + y).val(0)
        alert('Between 1 & 9 ploX');
        return;
    }
    localStorage.setItem('bid', '.box-' + x + '-' + y)
    //$('.lastBox').attr('bid', '.box-' + x + '-' + y)
    sendNum(x,y, value)
    location.reload();
}

const makeNewPosition = () => {

    // Get viewport dimensions (remove the dimension of the div)
    var h = $(window).height() - 50;
    var w = $(window).width() - 50;

    var nh = Math.floor(Math.random() * h);
    var nw = Math.floor(Math.random() * w);

    return [nh, nw];

}


const calcSpeed = (prev, next) => {

    var x = Math.abs(prev[1] - next[1]);
    var y = Math.abs(prev[0] - next[0]);

    var greatest = x > y ? x : y;

    var speedModifier = Math.random() / 1;

    var speed = Math.ceil(greatest / speedModifier);

    return speed;

}

const animateDiv = () => {
    var newq = makeNewPosition();
    var oldq = $('.lol').offset();
    var speed = calcSpeed([oldq.top, oldq.left], newq);

    $('.lol').animate({ top: newq[0], left: newq[1] }, speed, function () {
        animateDiv();
    });

};

(() => {
    animateDiv();
    $('.BoxState').on('click', '.BlockUndo', () => {
        console.log('click')
        let lb = localStorage.getItem('bid')
        let nums = /.{3}$/.exec(lb)
        let ids = nums.toString().split('-')

        sendNum(ids[0], ids[1], 0)
        location.reload()
        //$(lb).val(0)
    })
})();