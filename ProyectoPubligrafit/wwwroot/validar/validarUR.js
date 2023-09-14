// Validar Usuarios Daniel

//Ventana
function ventana(){
  window.open('asignar', 'Roles', 'left=400,top=150,width=800,height=500')
}

//Pruebas de estado
function inicializarEstados() {
    // Iterar a través de todos los elementos con ID switch-label-
    // y establecer su estado según el localStorage
    const switches = document.querySelectorAll('[id^="switch-label-"]');
    switches.forEach(switchElement => {
        const id = switchElement.id.split('-')[2]; // Obtener el ID del producto
        const estadoGuardado = localStorage.getItem('switchEstado-' + id);
        if (estadoGuardado !== null) {
            // Si hay un estado guardado en el localStorage, establecer el estado del interruptor
            switchElement.checked = estadoGuardado === "false"; // Convierte el valor de cadena en un booleano
        }


    });

}

// Esta función cambia el estado y guarda en el localStorage
function estado(id) {
    const validar = document.getElementById('switch-label-' + id);
    const estadoActual = validar.checked;

    // Cambiar el estado y guardar en el localStorage
    localStorage.setItem('switchEstado-' + id, !estadoActual);

    // Enviar una solicitud AJAX para cambiar el estado en el servidor
    $.ajax({
        url: '/Usuario/CambiarEstado',
        type: 'POST',
        data: { productoId: id, nuevoEstado: !estadoActual },
        success: function (result) {
            // Manejar la respuesta si es necesario
            console.log(result);
        },
        error: function (error) {
            console.error(error);
        }
    });



}

// Llamar a la función para inicializar los estados cuando la página se carga
document.addEventListener('DOMContentLoaded', inicializarEstados); 

function validart(){

    
    let vnombre=document.getElementById("nombres").value;
    let vapellido=document.getElementById("apellidos").value;
    
    let vcorreo=document.getElementById("correo").value;
    let vcontrasena=document.getElementById("contrasena").value;
    let vconfirmar=document.getElementById("confirmar").value;

    emailRegex = /^[-\w.%+]{1,64}@(?:[A-Z0-9-]{1,63}\.){1,125}[A-Z]{2,63}$/;
    let regex = /^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$/;
    
   if( vnombre=="" || vapellido==""||vcorreo==""||vcontrasena==""||vconfirmar==""){

        Swal.fire({
            icon: 'error',
            title: 'Campos Vacios',
            text: 'Por favor ingresar datos!',
            
          })


      
    }else if(!regex.test(vnombre)){
      Swal.fire({
        icon: 'error',
        title: 'Campo No Valido',
        text: 'Por favor ingresar datos!',
        
      })
    }else if(!regex.test(vapellido)){
      Swal.fire({
        icon: 'error',
        title: 'Campo No Valido',
        text: 'Por favor ingresar datos!',
        
      })
    }
    
    else if(!vcorreo.includes("@") || !vcorreo.includes(".com")){
        Swal.fire({
            icon: 'error',
            title: 'Correo no valido',
            text: 'Por favor ingresar un correo valido!',
            
          })
    }else if(vcontrasena!==vconfirmar){
        Swal.fire({
            icon: 'error',
            title: 'La contraseña no coincide ',
            text: 'Verifique que sus contraseñas sean iguales!',
            
          })

    }else{
        const swalWithBootstrapButtons = Swal.mixin({
            customClass: {
              confirmButton: 'btn btn-success',
              cancelButton: 'btn btn-danger'
            },
            buttonsStyling: false
          })
          
          swalWithBootstrapButtons.fire({
            title: 'Confirmar el envio del formulario?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Aceptar!',
            cancelButtonText: 'Cancelar!',
            Buttons: true
          }).then((result) => {
            if (result.isConfirmed) {

              //Linea de codigo muy importante para el cambio de type button a submit
              const formulario=document.getElementById('pruebas');
              formulario.submit();

              swalWithBootstrapButtons.fire(
                'Registro Enviado!',
                'Your file has been deleted.',
                'success'
              )
            } else if (
              /* Read more about handling dismissals below */
              result.dismiss === Swal.DismissReason.cancel
            ) {
              swalWithBootstrapButtons.fire(
                'Se cancelo el envio',
                'Your imaginary file is safe :)',
                'error'
              )
            }
           
          })
         
         
        }
    }


eliminarRol = (id) => {

    Swal.fire({
        title: 'Eliminar Registro?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Aceptar'
    }).then((result) => {
        if (result.isConfirmed) {

            window.location = '/Rol/Delete/' + id;
        }
    })
}