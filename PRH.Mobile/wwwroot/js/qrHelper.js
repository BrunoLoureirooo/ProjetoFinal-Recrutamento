var qrCode;

window.qrGenerator = {

       initializeQrCode: function (container) {
        var containerElement = document.getElementById(container);

        if (containerElement !== null && qrCode === undefined) {
            qrCode = new QRious({element: containerElement, size: 300});
        }
    },

    generateQrCode: function (data) {
        // stringify json the data
       const jsonData = JSON.stringify(data);
       qrCode.set({value: jsonData});
    },

    clearQrCode: function () {
        qrCode.set({value: ''});
    }

}

var qrScanner;
window.qrScanHelper = {
    startScan: function (dotNetObject) {
        qrScanner = new QrScanner(
            document.getElementById("qrScanner"),
            result => {
                console.log('decoded qr code:', result)
                dotNetObject.invokeMethodAsync('OnQrCodeScanned', result.data);
            },
            { 
                video: { facingMode: { exact: "environment" } },
                highlightScanRegion: true,
                highlightCodeOutline: true,
              
            },
        );
        qrScanner.start();
        

    },

     stopScan: function () {
         qrScanner.stop();
         qrScanner.destroy();
         qrScanner = null;
    }
};