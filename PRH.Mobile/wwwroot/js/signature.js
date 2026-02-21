window.downloadSignature = function (fileName, base64Data) {
    const link = document.createElement('a');
    link.download = fileName;
    link.href = 'data:image/png;base64,' + base64Data;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

window.hideSignaturePadClearButton = () => {
    const buttons = document.querySelectorAll('button');
    buttons.forEach(btn => {
        if (btn.textContent.trim().toLowerCase() === 'clear') {
            btn.style.display = 'none';
        }
    });
};