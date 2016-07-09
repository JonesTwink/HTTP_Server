            var address;

            function sendGet() {
                   address = document.getElementById('address').value; 

                   var jqxhr = $.post( "http://"+address, function(data) {
                    alert( "Отправлен GET на ресурс "+address);
                    window.open("http://"+address,"_self")                 
     
                    })

            }
            function sendPost() {
                   address = document.getElementById('address').value; 
                   $.post("http://"+address, {a: 'this', b:'is', c: 'post'});
                   alert( "Отправлен POST на ресурс "+address);
            }
            function sendPut() {
                   address = document.getElementById('address').value; 
                   $.ajax({
                    url: "http://"+address,
                    method: 'PUT',
                    data: {a: 'this', b: 'is', c: 'PUT'},
                    success: function(result) {
                        alert( "Отправлен PUT на ресурс "+address);
                    }
                    });            
            }

            function sendDelete() {
                   address = document.getElementById('address').value; 
                   $.ajax({
                    url: "http://"+address,
                    method: 'DELETE',                    
                    success: function(result) {
                        alert( "Отправлен DELETE на ресурс "+address);
                    }
                    });            
            }

