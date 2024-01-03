function mostrarNotificacionExito(notificacion) {
    toastr.success(notificacion, 'Success!');
}

function mostrarNotificacionWarning(notificacion) {
    toastr.warning(notificacion, 'Warning!');
}

function mostrarNotificacionError(notificacion) {
    toastr.error(notificacion, 'Error!');
}