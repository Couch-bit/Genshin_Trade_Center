function scroll1(){
    $([document.documentElement, document.body]).animate({
        scrollTop: $("#sekcja1").offset().top - 35
    }, 2000);
}


$(window).scroll(function() {
    var scroll = $(window).scrollTop();
    var ograniczenie = $("#sekcja1").offset().top - 35;
    if(scroll > ograniczenie) {
        $("#side-contain").slideDown("slow");
    }
    if(scroll < ograniczenie){
        $("#side-contain").slideUp("slow");
    }
});

$("#song-on").hide();
$("#song-off").show();
$(document).ready(function(){
    $("#song-change").click(function(){
        var audio = $(".audio")[0];
        if (audio.paused) {
            audio.play();
            $("#song-on").show();
            $("#song-off").hide();
        }  else {
            audio.pause();
            $("#song-on").hide();
            $("#song-off").show();
        }
    });
});

const button = document.getElementById('button');
const emoji = document.getElementById('emoji');

button.addEventListener('click', () => {
  emoji.style.display = 'block';
  setTimeout(() => {
    emoji.style.display = 'none';
  }, 2000);
});

// Get all buttons with the "nice-button" class
const buttons = document.querySelectorAll('.nice-button');

// Add click event listener to each button
buttons.forEach(button => {
    button.addEventListener('click', () => {
        // Disable the clicked button
        button.disabled = true;

        // Enable the button after one second
        setTimeout(() => {
            button.disabled = false;
        }, 1000);
    });
});

