/** @type {import('tailwindcss').Config} */
module.exports = {
    prefix: 'tw-',
    content: [
        "./Components/**/*.{razor,html,cshtml}",
        "../Guldaan.Common.RazorUI/**/*.razor",
        "../Guldaan.Security.UI.Client/**/*.razor"
    ],
    safelist: [
        'tw-hidden',
        'tw-block'
    ],
    theme: {
        extend: {},
    },
    plugins: [],
}
