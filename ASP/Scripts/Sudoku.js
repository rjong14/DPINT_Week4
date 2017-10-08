const sendNum = (x, y, value) => {
    $.ajax({
        url: "/Home/SetValue/?posx=" + x + "&posy=" + y + "&value=" + value,
        type: "GET",
        error: function (response) {
            alert("Er ging iets mis bij het opslaan van de waarde");
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
        alert('Voer een getal in.');
        return;
    }
    if (value < 1 || value > 9) {
        //txtInput.val(0);
        $('.box-' + x + '-' + y).val(0)
        alert('Voer een getal in van 1 t/m 9.');
        return;
    }
    localStorage.setItem('bid', '.box-' + x + '-' + y)
    //$('.lastBox').attr('bid', '.box-' + x + '-' + y)
    //Ajax call to method, we don't want a postback to occur
    sendNum(x,y, value)
    location.reload();
}

(() => {
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