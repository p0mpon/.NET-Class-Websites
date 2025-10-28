document.addEventListener("DOMContentLoaded", () => {
    const canvases = document.querySelectorAll("canvas.canvasLines");

    canvases.forEach(canvas => {
        const ctx = canvas.getContext("2d");

        function drawLines(e) {
            canvas.width = canvas.clientWidth;
            canvas.height = canvas.clientHeight;

            const rect = canvas.getBoundingClientRect();
            const x = e.clientX - rect.left;
            const y = e.clientY - rect.top;

            ctx.clearRect(0, 0, canvas.width, canvas.height);
            ctx.beginPath();

            ctx.moveTo(0, 0);
            ctx.lineTo(x, y);

            ctx.moveTo(canvas.width, 0);
            ctx.lineTo(x, y);

            ctx.moveTo(0, canvas.height);
            ctx.lineTo(x, y);

            ctx.moveTo(canvas.width, canvas.height);
            ctx.lineTo(x, y);

            ctx.lineWidth = 4;
            ctx.strokeStyle = "pink";
            ctx.stroke();
        }

        function clearCanvas() {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            ctx.beginPath();
        }

        canvas.addEventListener("mousemove", drawLines);
        canvas.addEventListener("mouseout", clearCanvas);
    });
});