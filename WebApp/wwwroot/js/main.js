/**
 * Template Name: FlexStart
 * Template URL: https://bootstrapmade.com/flexstart-bootstrap-startup-template/
 * Updated: Nov 01 2024 with Bootstrap v5.3.3
 * Author: BootstrapMade.com
 * License: https://bootstrapmade.com/license/
 */

window.initializeFlexStart = function () {
    "use strict";

    /**
     * Apply .scrolled class to the body as the page is scrolled down
     */
    function toggleScrolled() {
        const selectBody = document.querySelector('body');
        const selectHeader = document.querySelector('#header');
        if (!selectHeader) {
            console.warn('Elemento #header no encontrado');
            return;
        }
        if (
            !selectHeader.classList.contains('scroll-up-sticky') &&
            !selectHeader.classList.contains('sticky-top') &&
            !selectHeader.classList.contains('fixed-top')
        )
            return;
        window.scrollY > 100 ? selectBody.classList.add('scrolled') : selectBody.classList.remove('scrolled');
    }

    document.addEventListener('scroll', toggleScrolled);
    toggleScrolled();

    /**
     * Mobile nav toggle
     */
    const mobileNavToggleBtn = document.querySelector('.mobile-nav-toggle');
    function mobileNavToogle() {
        document.querySelector('body').classList.toggle('mobile-nav-active');
        mobileNavToggleBtn.classList.toggle('bi-list');
        mobileNavToggleBtn.classList.toggle('bi-x');
    }
    if (mobileNavToggleBtn) {
        mobileNavToggleBtn.addEventListener('click', mobileNavToogle);
    } else {
        console.warn('Elemento .mobile-nav-toggle no encontrado');
    }

    /**
     * Hide mobile nav on same-page/hash links
     */
    document.querySelectorAll('#navmenu a').forEach((navmenu) => {
        navmenu.addEventListener('click', () => {
            if (document.querySelector('.mobile-nav-active')) {
                mobileNavToogle();
            }
        });
    });

    /**
     * Toggle mobile nav dropdowns
     */
    document.querySelectorAll('.navmenu .toggle-dropdown').forEach((navmenu) => {
        navmenu.addEventListener('click', function (e) {
            e.preventDefault();
            this.parentNode.classList.toggle('active');
            this.parentNode.nextElementSibling.classList.toggle('dropdown-active');
            e.stopImmediatePropagation();
        });
    });

    /**
     * Scroll top button
     */
    let scrollTop = document.querySelector('.scroll-top');
    function toggleScrollTop() {
        if (scrollTop) {
            window.scrollY > 100 ? scrollTop.classList.add('active') : scrollTop.classList.remove('active');
        } else {
            console.warn('Elemento .scroll-top no encontrado');
        }
    }
    if (scrollTop) {
        scrollTop.addEventListener('click', (e) => {
            e.preventDefault();
            window.scrollTo({
                top: 0,
                behavior: 'smooth'
            });
        });
    } else {
        console.warn('Elemento .scroll-top no encontrado para el evento click');
    }

    document.addEventListener('scroll', toggleScrollTop);
    toggleScrollTop(); // Ejecuta inmediatamente por si la página ya está desplazada

    /**
     * Animation on scroll function and init
     */
    function aosInit() {
        if (typeof AOS === 'undefined') {
            console.warn('AOS no está definido');
            return;
        }
        AOS.init({
            duration: 600,
            easing: 'ease-in-out',
            once: true,
            mirror: false
        });
    }
    aosInit();

    /**
     * Initiate glightbox
     */
    let glightbox;
    if (typeof GLightbox !== 'undefined') {
        glightbox = GLightbox({
            selector: '.glightbox'
        });
    } else {
        console.warn('GLightbox no está definido');
    }

    /**
     * Initiate Pure Counter
     */
    if (typeof PureCounter !== 'undefined') {
        new PureCounter();
    } else {
        console.warn('PureCounter no está definido');
    }

    /**
     * Frequently Asked Questions Toggle
     */
    document.querySelectorAll('.faq-item h3, .faq-item .faq-toggle').forEach((faqItem) => {
        faqItem.addEventListener('click', () => {
            faqItem.parentNode.classList.toggle('faq-active');
        });
    });

    /**
     * Init isotope layout and filters
     */
    document.querySelectorAll('.isotope-layout').forEach(function (isotopeItem) {
        if (typeof Isotope === 'undefined' || typeof imagesLoaded === 'undefined') {
            console.warn('Isotope o imagesLoaded no está definido');
            return;
        }
        let layout = isotopeItem.getAttribute('data-layout') ?? 'masonry';
        let filter = isotopeItem.getAttribute('data-default-filter') ?? '*';
        let sort = isotopeItem.getAttribute('data-sort') ?? 'original-order';

        let initIsotope;
        imagesLoaded(isotopeItem.querySelector('.isotope-container'), function () {
            initIsotope = new Isotope(isotopeItem.querySelector('.isotope-container'), {
                itemSelector: '.isotope-item',
                layoutMode: layout,
                filter: filter,
                sortBy: sort
            });
        });

        isotopeItem.querySelectorAll('.isotope-filters li').forEach(function (filters) {
            filters.addEventListener('click', function () {
                isotopeItem.querySelector('.isotope-filters .filter-active').classList.remove('filter-active');
                this.classList.add('filter-active');
                initIsotope.arrange({
                    filter: this.getAttribute('data-filter')
                });
                if (typeof aosInit === 'function') {
                    aosInit();
                }
            }, false);
        });
    });

    /**
     * Init swiper sliders
     */
    function initSwiper() {
        if (typeof Swiper === 'undefined') {
            console.warn('Swiper no está definido');
            return;
        }
        document.querySelectorAll('.init-swiper').forEach(function (swiperElement) {
            let config = JSON.parse(swiperElement.querySelector('.swiper-config').innerHTML.trim());
            if (swiperElement.classList.contains('swiper-tab')) {
                initSwiperWithCustomPagination(swiperElement, config);
            } else {
                new Swiper(swiperElement, config);
            }
        });
    }
    initSwiper();

    /**
     * Correct scrolling position upon page load for URLs containing hash links
     */
    if (window.location.hash) {
        if (document.querySelector(window.location.hash)) {
            setTimeout(() => {
                let section = document.querySelector(window.location.hash);
                let scrollMarginTop = getComputedStyle(section).scrollMarginTop;
                window.scrollTo({
                    top: section.offsetTop - parseInt(scrollMarginTop),
                    behavior: 'smooth'
                });
            }, 100);
        }
    }

    /**
     * Navmenu Scrollspy
     */
    let navmenulinks = document.querySelectorAll('.navmenu a');

    function navmenuScrollspy() {
        let isAnySectionActive = false;        
        // Iterar sobre los enlaces para determinar si alguna sección está activa
        navmenulinks.forEach((navmenulink) => {
            if (!navmenulink.hash) return;

            let section = document.querySelector(navmenulink.hash);
            if (!section) return;

            let position = window.scrollY + 200;
            if (position >= section.offsetTop && position <= section.offsetTop + section.offsetHeight) {
                isAnySectionActive = true;
                // Eliminar 'active' de todos los enlaces
                document.querySelectorAll('.navmenu a.active').forEach((link) => link.classList.remove('active'));
                // Agregar 'active' al enlace actual
                navmenulink.classList.add('active');
            }
        });

        // Si ninguna sección está activa, eliminar 'active' de todos los enlaces
        if (!isAnySectionActive) {
            document.querySelectorAll('.navmenu a.active').forEach((link) => link.classList.remove('active'));
        }
    }

    document.addEventListener('scroll', navmenuScrollspy);
    navmenuScrollspy();
};