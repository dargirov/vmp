// Initialize and add the map
function initMap() {

    var mapElement = document.getElementById('map');
    var placesData = JSON.parse(mapElement.dataset.places);

    var sofia = { lat: 42.69185653916361, lng: 23.31725440539553 };
    var pathForCheckIcon = 'M10.453 14.016l6.563-6.609-1.406-1.406-5.156 5.203-2.063-2.109-1.406 1.406zM12 2.016q2.906 0 4.945 2.039t2.039 4.945q0 1.453-0.727 3.328t-1.758 3.516-2.039 3.070-1.711 2.273l-0.75 0.797q-0.281-0.328-0.75-0.867t-1.688-2.156-2.133-3.141-1.664-3.445-0.75-3.375q0-2.906 2.039-4.945t4.945-2.039z';
    var pathForEmptyIcon = 'M12 2.016q2.906 0 4.945 2.039t2.039 4.945q0 1.453-0.727 3.328t-1.758 3.516-2.039 3.070-1.711 2.273l-0.75 0.797q-0.281-0.328-0.75-0.867t-1.688-2.156-2.133-3.141-1.664-3.445-0.75-3.375q0-2.906 2.039-4.945t4.945-2.039z';

    var map = new google.maps.Map(mapElement, {
        zoom: 15,
        center: placesData.length === 1 ? { lat: parseFloat(placesData[0].Lat), lng: parseFloat(placesData[0].Lng) } : sofia,
        styles: [ 
            { featureType: 'poi', elementType: 'labels', stylers: [ { visibility: 'off' } ] },
            { featureType: 'transit', stylers: [ { visibility: 'off' } ] },
        ]
    });

    var svgMarkerGreen = {
        path: pathForCheckIcon,
        fillColor: 'green',
        fillOpacity: 0.8,
        strokeWeight: 0,
        rotation: 0,
        scale: 2,
        anchor: new google.maps.Point(15, 30),
    };

    var svgMarkerYellow = Object.assign({}, svgMarkerGreen);
    svgMarkerYellow.fillColor = '#fba41d';
    svgMarkerYellow.path = pathForEmptyIcon;

    function attachInfoWindowsWithMessage(marker, message) {

        var infowindow = new google.maps.InfoWindow({
            content: message,
        });

        marker.addListener('click', () => {
            map.setZoom(16);
            map.setCenter(marker.getPosition());
            infowindow.open(map, marker);
        });
    }

    for (var i = 0; i < placesData.length; i++) {

        var marker = new google.maps.Marker({
            position: { lat: parseFloat(placesData[i].Lat), lng: parseFloat(placesData[i].Lng) },
            map: map,
            icon: parseInt(placesData[i].type) === 0 ? svgMarkerGreen : svgMarkerYellow,
            //label: placesData[i].Name
        });

        attachInfoWindowsWithMessage(marker, placesData[i].Name + '<br>' + placesData[i].Address + '<br><a href="/place/' + placesData[i].Acronym + '">Виж повече</a>');
    }

    /*
    // The marker, positioned at edgy veggie
    var marker1 = new google.maps.Marker({
        position: sofia,
        map: map,
        icon: svgMarkerGreen,
        label: 'E'
    });

    marker1.addListener('click', () => {
        map.setZoom(16);
        map.setCenter(marker1.getPosition());
        infowindow.setContent('Edgy Veggy<br>ул. "Уилям Гладстон" 18');
        infowindow.open(map, marker1);
    });

    // The marker, positioned at satsanga
    var marker2 = new google.maps.Marker({
        position: { lat: 42.69515684146619, lng: 23.328382181005356 },
        map: map,
        icon: svgMarkerYellow,
        label: 'S'
    });

    marker2.addListener('click', () => {
        map.setZoom(16);
        map.setCenter(marker2.getPosition());
        infowindow.setContent('Satsanga<br>ул. "Уилям Гладстон" 18<br><a href="#">Виж повече</a>');
        infowindow.open(map, marker2);
    });
    */
    
  }


(function() {

    var mainMenu = document.getElementById('main-menu');

    var dropDownLinks = document.querySelectorAll('.dropdown-container > a');
    for (var i = 0; i < dropDownLinks.length; i++) {
        dropDownLinks[i].onclick = function(e) {
            e.preventDefault();
            var dd = this.parentNode.querySelector('.dropdown');
            dd.classList.remove('hidden');
            setTimeout(function() {
                dd.style.top = '32px';
            }, 10);
            return false;
        }
    }

    document.body.addEventListener('mouseup', function(e) {
        var dd = this.parentNode.querySelectorAll('.dropdown');
        for (var i = 0; i < dd.length; i++) {
            if (!dd[i].classList.contains('hidden') && !isDescendant(dd[i], e.target)) {
                dd[i].classList.add('hidden');
                dd[i].style.top = '60px';
            }
        }

        if (menuOpened && !isDescendant(mainMenu, e.target)) {
            document.querySelector('#bars a').click();
        }
    });

    var placesList = document.querySelectorAll('#main-container-places > ul > li');
    var filterCheckboxes = document.querySelectorAll('#main-container-filter input');
    for (var i = 0; i < filterCheckboxes.length; i++) {
        filterCheckboxes[i].onchange = function (e) {
            var type1Number = parseInt(filterCheckboxes[0].dataset.type);
            var type1Checked = filterCheckboxes[0].checked;
            var type2Number = parseInt(filterCheckboxes[1].dataset.type);
            var type2Checked = filterCheckboxes[1].checked;

            for (var j = 0; j < placesList.length; j++) {
                var placeType = parseInt(placesList[j].dataset.type);
                if (placeType === type1Number && type1Checked) {
                    placesList[j].classList.remove('hidden');
                } else if (placeType === type2Number && type2Checked) {
                    placesList[j].classList.remove('hidden');
                } else {
                    placesList[j].classList.add('hidden');
                }
            }
        }
    }

    var galleryLinks = document.getElementsByClassName('gallery');
    for (var i = 0; i < galleryLinks.length; i++) {
        galleryLinks[i].onclick = function (e) {
            e.preventDefault();

            var pswpDiv = document.createElement('div');
            pswpDiv.classList.add('pswp');

            var pswp__bgDiv = document.createElement('div');
            pswp__bgDiv.classList.add('pswp__bg');
            pswpDiv.appendChild(pswp__bgDiv);

            var pswp__scroll_wrapDiv = document.createElement('div');
            pswp__scroll_wrapDiv.classList.add('pswp__scroll-wrap');
            pswpDiv.appendChild(pswp__scroll_wrapDiv);

            var pswp__containerDiv = document.createElement('div');
            pswp__containerDiv.classList.add('pswp__container');
            pswp__scroll_wrapDiv.appendChild(pswp__containerDiv);

            for (var i = 0; i < 3; i++) {
                var pswp__itemDiv = document.createElement('div');
                pswp__itemDiv.classList.add('pswp__item');
                pswp__containerDiv.appendChild(pswp__itemDiv);
            }

            var pswp__uiDiv = document.createElement('div');
            pswp__uiDiv.classList.add('pswp__ui');
            pswp__uiDiv.classList.add('pswp__ui--hidden');
            pswp__scroll_wrapDiv.appendChild(pswp__uiDiv);

            var pswp__top_barDiv = document.createElement('div');
            pswp__top_barDiv.classList.add('pswp__top-bar');
            pswp__uiDiv.appendChild(pswp__top_barDiv);

            var pswp__counterDiv = document.createElement('div');
            pswp__counterDiv.classList.add('pswp__counter');
            pswp__top_barDiv.appendChild(pswp__counterDiv);

            var pswp__button_closeBtn = document.createElement('button');
            pswp__button_closeBtn.classList.add('pswp__button');
            pswp__button_closeBtn.classList.add('pswp__button--close');
            pswp__top_barDiv.appendChild(pswp__button_closeBtn);

            var pswp__preloaderDiv = document.createElement('div');
            pswp__preloaderDiv.classList.add('pswp__preloader');
            pswp__top_barDiv.appendChild(pswp__preloaderDiv);

            var pswp__preloader__icnDiv = document.createElement('div');
            pswp__preloader__icnDiv.classList.add('pswp__preloader__icn');
            pswp__preloaderDiv.appendChild(pswp__preloader__icnDiv);

            var pswp__preloader__cutDiv = document.createElement('div');
            pswp__preloader__cutDiv.classList.add('pswp__preloader__cut');
            pswp__preloader__icnDiv.appendChild(pswp__preloader__cutDiv);

            var pswp__preloader__donutDiv = document.createElement('div');
            pswp__preloader__donutDiv.classList.add('pswp__preloader__donut');
            pswp__preloader__cutDiv.appendChild(pswp__preloader__donutDiv);

            var pswp__captionDiv = document.createElement('div');
            pswp__captionDiv.classList.add('pswp__caption');
            pswp__uiDiv.appendChild(pswp__captionDiv);

            var pswp__caption__centerDiv = document.createElement('div');
            pswp__caption__centerDiv.classList.add('pswp__caption__center');
            pswp__captionDiv.appendChild(pswp__caption__centerDiv);

            document.querySelector('body').appendChild(pswpDiv);
            var pswpElement = pswpDiv;

            var items = [];

            var images = document.querySelectorAll('#place-container-left-pictures img');
            for (var i = 0; i < images.length; i++) {
                items.push({ src: images[i].dataset.src, w: images[i].dataset.width, h: images[i].dataset.height });
            }

            var options = { index: parseInt(this.dataset.index), bgOpacity: 0.8, history: false, tapToToggleControls: false, indexIndicatorSep: ' от ' };

            var gallery = new PhotoSwipe(pswpElement, PhotoSwipeUI_Default, items, options);
            gallery.init();
            gallery.listen('destroy', function () { document.querySelector('.pswp').remove(); });

            return false;
        }
    }

    var menuOpened = false;
    var barsBtn = document.querySelector('#bars a');
    barsBtn.onclick = function (e) {
        e.preventDefault();
        if (menuOpened) {
            mainMenu.style.width = '0';
        } else {
            mainMenu.style.width = '80%';
        }

        menuOpened = !menuOpened;
        return false;
    }

})();