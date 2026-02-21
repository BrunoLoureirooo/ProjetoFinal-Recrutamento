window.cryptoHelper = {
    signData: async function (privateKey, data) {
        const key = await window.crypto.subtle.importKey(
            "pkcs8",
            Uint8Array.from(atob(privateKey), c => c.charCodeAt(0)),
            {
                name: "RSA-PSS",
                hash: { name: "SHA-256" }
            },
            false,
            ["sign"]
        );

        const signature = await window.crypto.subtle.sign(
            {
                name: "RSA-PSS",
                saltLength: 32
            },
            key,
            new TextEncoder().encode(data)
        );

        return btoa(String.fromCharCode(...new Uint8Array(signature)));
    },

    verifySignature: async function (publicKey, data, signature) {
        const key = await window.crypto.subtle.importKey(
            "spki",
            Uint8Array.from(atob(publicKey), c => c.charCodeAt(0)),
            {
                name: "RSA-PSS",
                hash: { name: "SHA-256" }
            },
            false,
            ["verify"]
        );

        const isValid = await window.crypto.subtle.verify(
            {
                name: "RSA-PSS",
                saltLength: 32
            },
            key,
            Uint8Array.from(atob(signature), c => c.charCodeAt(0)),
            new TextEncoder().encode(data)
        );

        return isValid;
    }

    
};