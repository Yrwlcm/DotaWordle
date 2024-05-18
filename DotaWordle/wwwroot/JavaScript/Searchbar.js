$(document).ready(function () {
    const results = $('#results');
    const items = results.find('.result-item');

    // Показать всех героев при фокусе на поле ввода, если поле пустое
    $('#search-input').on('focus', function () {
        const filter = $(this).val().toLowerCase();
        if (filter) {
            filterHeroes(filter);
        } else {
            results.show();
            items.show();
        }
    });

    // Фильтрация героев при вводе текста
    $('#search-input').on('input', function () {
        const filter = $(this).val().toLowerCase();
        filterHeroes(filter);
    });

    // Функция фильтрации героев
    function filterHeroes(filter) {
        if (filter) {
            results.show();
            let count = 0;
            items.each(function () {
                const item = $(this);
                const text = item.find('span').text().toLowerCase();
                if (text.indexOf(filter) > -1 && count < 5) {
                    item.show();
                    count++;
                } else {
                    item.hide();
                }
            });
        } else {
            results.hide();
        }
    }

    // Скрыть результаты при клике вне области поиска
    $(document).on('click', function (e) {
        if (!$(e.target).closest('.search-container').length) {
            results.hide();
        }
    });
    
    // Post запрос для сравнения героев
    items.on('click', function () {
        const heroId = $(this).data('id');
        const antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();
        
        console.log(heroId);
        $.ajax({
            url: `api/heroes/compare/hiddenHero/${heroId}`,
            method: 'GET',
            data: {
                __RequestVerificationToken: antiForgeryToken
            },
            success: function (response) {
                alert('Герой выбран успешно!');
                console.log(response);
            },
            error: function (xhr, status, error) {
                alert('Произошла ошибка при выборе героя.');
            }
        });
    });
});