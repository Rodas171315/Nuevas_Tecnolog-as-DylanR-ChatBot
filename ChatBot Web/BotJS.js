

var UserName = "Usuario";

function ResetChat(){
    
}

function SendChat(Mensaje){
    
    var prevMensaje = $("#tabchat").html();
    
        $("#tabchat").html(prevMensaje + "<span class = 'Mensaje_actual>"+"<span class = 'Bot'> TacoBot: </span>"+ Mensaje +"</span>"+"<br>");
        $(".Mensaje_actual").hide();
        $(".Mensaje_actual").delay(500).fadeIn();
        $(".Mensaje_actual").removeClass("Mensaje_actual");    

    function Respuesta(Mensaje){
        UserName = Mensaje;
        SendChat("Mucho gusto Sr. "+UserName+", Como puedo ayudarle?");
    }
    
}

function Speak(){

}

$("document").ready(function(){

    SendChat();
    //Envia el texto clickeando Enter 
    $("#textBox").keypress(function(event){
        if(event.which == 13){
            if($("#checkbox").prop("checked")){
                $("#SendButton").click();
                event.preventDefault();
            }
        }
    });

    $("#SendButton").click(function(){

        var userName = "<span class = 'userName'> You </span>";

        var txt = $("#textBox").val();
        $("#textBox").val(" ");
     
        var prevMensaje = $("#tabchat").html();

        $("#tabchat").html(prevMensaje + UserName + ": " + txt + "<br>");
        
        
    });
});
