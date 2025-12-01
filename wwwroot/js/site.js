function mostrarVideo(ruta) {
    console.log("Reproducir:", ruta);

    const video = document.querySelector(".video");

    if (video) {
        video.src = ruta;
        video.load();
        video.play();
    } else {
        console.error("No se encontró el video");
    }
}