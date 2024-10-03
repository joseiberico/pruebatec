// Función para registrar la venta
function registrarVenta() {
        var detalle_venta = [];
        var total = 0;

        // Recorrer la tabla para recolectar los productos
        $('#tbProducto > tbody > tr').each(function () {
            var idProducto = parseInt($(this).find('td:eq(0)').text(), 10);
            var precio = parseFloat($(this).find('td:eq(2)').text());
            var cantidad = parseInt($(this).find('td:eq(3)').text(), 10);
            var totalDetalle = parseFloat($(this).find('td:eq(4)').text());

            detalle_venta.push({
                IdProducto: idProducto,
                Precio: precio,
                Cantidad: cantidad,
                Total: totalDetalle,
                IdEstado: 1 // Valor por defecto
            });

            total += totalDetalle;
        });

        // Verificar si hay productos en la tabla antes de registrar la venta
        if (detalle_venta.length === 0) {
            alert("Debes agregar al menos un producto antes de registrar la venta.");
            return;
        }

        // Crear el objeto venta
        var venta = {
            NumeroDocumento: $("#txtnumerodocumento").val(),
            RazonSocial: $("#txtrazonsocial").val(),
            Total: total,
            IdEstado: 1
        };

        // Crear el objeto con los detalles
        var detalleVenta = { "Elementos": detalle_venta };

        // Crear un objeto que contenga ambos objetos
        var requestData = {
            objVenta: venta,
            objDetalleVenta: detalleVenta
        };
        debugger;
        // Configuración de AJAX
        $.ajax({
            url: '/Venta/VentaInsertar',
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify(requestData), // Asegúrate de que el formato de datos sea correcto
            success: function (response) {
                console.log(response);
                console.log(requestData);
                if (response.respuesta) {
                    alert("Venta Registrada");

                    // Limpiar la tabla y los campos
                    $("#tbProducto tbody").empty();
                    $("#txtnumerodocumento").val("");
                    $("#txtrazonsocial").val("");

                    window.location.href = '/Venta/frmReporteVenta'; // Asegúrate de que esta URL sea correcta
                } else {
                    alert("Error al registrar la venta");
                }
            },
            error: function () {
                alert("Error en la solicitud");
            }
    });
}


$(document).ready(function () {
    // Obtener el precio del producto seleccionado
    $("#cmbProducto").on("change", function () {
        var productoId = $(this).val();
        if (productoId) {
            // Llamada AJAX para obtener el precio del producto
            $.ajax({
                url: '/Venta/ObtenerPrecioProducto', // URL directa
                type: "GET",
                data: { id: productoId },
                success: function (data) {
                    $("#txtprecio").val(data.precio); // Asignar el precio obtenido
                },
                error: function () {
                    alert("Error al obtener el precio del producto.");
                }
            });
        } else {
            $("#txtprecio").val(""); // Limpiar el campo si no hay selección
        }
    });

    // Agregar producto a la tabla
    $("#btnAgregar").on("click", function () {
        var productoId = $("#cmbProducto").val(); // Obtener el Id del producto seleccionado
        var productoNombre = $("#cmbProducto option:selected").text(); // Obtener el nombre del producto seleccionado
        var precio = parseFloat($("#txtprecio").val());
        var cantidad = parseInt($("#txtcantidad").val());
        var total = precio * cantidad;

        // Verificar si los campos están llenos antes de agregar
        if (!productoId || !precio || !cantidad) {
            alert("Por favor, selecciona un producto y completa los campos.");
            return;
        }

        $("#tbProducto tbody").append(
            $("<tr>").append(
                $("<td>").text(productoId),          // Agregar IdProducto en la primera celda
                $("<td>").text(productoNombre),      // Nombre del producto
                $("<td>").text(precio.toFixed(2)),   // Precio
                $("<td>").text(cantidad),            // Cantidad
                $("<td>").text(total.toFixed(2))     // Total
            )
        );

        // Limpiar campos
        $("#cmbProducto").val("");
        $("#txtprecio").val("");
        $("#txtcantidad").val("");
        $("#cmbProducto").focus();
    });

    // Registrar la venta
    $("#btnTerminar").on("click", function () {
        registrarVenta(); // Llama a la función para registrar la venta
    
    });
});