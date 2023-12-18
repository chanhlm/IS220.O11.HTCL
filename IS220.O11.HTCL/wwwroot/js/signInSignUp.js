$(document).ready(function () {
    const login = document.querySelector('.open-login')
    const sign_up = document.querySelector('.open-signup')
    const cart = document.querySelector('.header__cart-icon')
    const notify = document.querySelector('.open-login-noti')
    const modal = document.querySelector('.modal')
    const modals = document.querySelector('.modal-logup')
    const modalClose = document.querySelector('.modal-close')
    const modalsClose = document.querySelector('.modals-close')

    function show() {
        modal.classList.add('open')
    }

    function shows() {
        modals.classList.add('open')
    }

    login.addEventListener('click', show)
    sign_up.addEventListener('click', shows)
    cart.addEventListener('click', show)
    notify.addEventListener('click', show)

    function hideshow() {
        modal.classList.remove('open')
    }
    modalClose.addEventListener('click', hideshow)

    function hideshows() {
        modals.classList.remove('open')
    }
    modalsClose.addEventListener('click', hideshows)
 });
