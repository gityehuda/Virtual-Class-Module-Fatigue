
function isSafari() {
    return /^((?!chrome|android).)*safari/i.test(navigator.userAgent);
}

function isNotChromeOrEdgeOnDesktop() {
    const ua = navigator.userAgent.toLowerCase();

    // Deteksi browser
    const isChrome = ua.includes("chrome") && !ua.includes("edg") && !ua.includes("opr");
    const isEdge = ua.includes("edg");

    // Deteksi perangkat mobile/tablet
    const isMobileOrTablet = /android|iphone|ipad|ipod|opera mini|iemobile|mobile|tablet/.test(ua);

    // Alert hanya jika bukan Chrome/Edge dan di desktop
    return !(isChrome || isEdge) && !isMobileOrTablet;
}