function CreateHeroCard(heroData) {
    return `
        <div class="card card-custom hero-card">
            <div class="row g-0 mx-5 text-center flex-row align-items-center justify-content-center border border-3 border-danger">
                <div class="col-md-3 d-flex align-items-center justify-content-center">
                    <img src="https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react/heroes/${heroData.name.replace(/ /g, "_").toLowerCase()}.png"
                         class="img-fluid rounded-start" alt="${heroData.name}">
                </div>
                <div class="col-md-1 d-flex align-items-center justify-content-center" id="primaryAttribute">
                    <img src="https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react/icons/hero_${heroData.primaryAttributeName.toLowerCase()}.png"
                         class="img-fluid rounded-start" alt="${heroData.primaryAttributeName}">
                </div>
                <div class="col-md-5">
                    <h5 class="card-title">${heroData.name}</h5>
                </div>
            </div>
            <div class="row g-0">
                <div class="card-body col-md-8 border border-3 border-danger">
                    <p class="text-center">
                        <strong>Stats:</strong>
                    </p>
                    <ul class="my-grid-container list-group list-group-flush border border-3 border-danger">
                        <li class="list-group-item" id="attackType">
                            <p>Attack type: ${heroData.attackType}</p>
                        </li>
                        <li class="list-group-item" id="attackRange">
                            <p>Attack range: ${heroData.attackRange}</p>
                        </li>
                        <li class="list-group-item">
                            <p>Attack damage: <span id="attackDamageMin">${heroData.startingDamageMin}</span>  - <span id="attackDamageMax">${heroData.startingDamageMax}</span> </p>
                        </li>
                        <li class="list-group-item" id="armor">
                            <p>Armor: ${Math.round(heroData.startingArmor * 10) / 10}</p>
                        </li>
                        <li class="list-group-item" id="movespeed">
                            <p>Move speed: ${heroData.startingMovespeed}</p>
                        </li>
                        <li class="list-group-item" id="complexity">
                            <p>Complexity: ${heroData.complexity}</p>
                        </li>
                        <li class="list-group-item" id="strengthBase">
                            <p>Strength: ${heroData.strengthBase}</p>
                        </li>
                        <li class="list-group-item" id="agilityBase">
                            <p>Agility: ${heroData.agilityBase}</p>
                        </li>
                        <li class="list-group-item" id="intelligenceBase">
                            <p>Intelligence: ${heroData.intelligenceBase}</p>
                        </li>
                        <li class="list-group-item"></li>
                    </ul>
                </div>
                <div class="card-body col-md-4 border border-3 border-danger">
                    <p class="card-text">
                        <p class="text-center">
                            <strong>Winrates:</strong>
                        </p>
                        <ul>
                            <li id="heraldWinrate">
                                <p>Herald: ${heroData.weekWinrates.find(w => w.rankBracket === "Herald").winrate}%</p>
                            </li>
                            <li id="legendWinrate">
                                <p>Legend: ${heroData.weekWinrates.find(w => w.rankBracket === "Legend").winrate}%</p>
                            </li>
                            <li id="immortalWinrate">
                                <p>Immortal: ${heroData.weekWinrates.find(w => w.rankBracket === "Immortal").winrate}%</p>
                            </li>
                        </ul>
                    </p>
                </div>
                <div class="card-body col-md-8 border border-3 border-danger">
                    <p class="text-center">
                        <strong>Roles:</strong>
                    </p>
                    <ul class="my-grid-container list-group list-group-flush border border-3 border-danger">
                        ${heroData.roles.map(role => `
                            <li class="list-group-item" id="${role.name}">
                                <p>${role.name}</p>
                                <div class="progress" role="progressbar" aria-label="${role.name}"
                                     aria-valuenow="${Math.round(role.level * 33.33)}" aria-valuemin="0" aria-valuemax="3">
                                    <div class="progress-bar" style="width: ${Math.round(role.level * 33.33)}%"></div>
                                </div>
                            </li>
                        `).join('')}
                        <li class="list-group-item"></li>
                    </ul>
                </div>
            </div>
        </div>
    `
}

function AddComparingStylesToHeroCard(heroElement, comparisionJson) {
    AddComparingStyle($('#primaryAttribute'), comparisionJson.samePrimaryAttribute);
    AddComparingStyle($('#attackType'), comparisionJson.sameAttackType);
    AddComparingStyle($('#attackRange'), comparisionJson.attackRangeComparision);
    AddComparingStyle($('#attackDamageMin'), comparisionJson.attackDamageMinComparision);
    AddComparingStyle($('#attackDamageMax'), comparisionJson.attackDamageMaxComparision);
    AddComparingStyle($('#armor'), comparisionJson.startingArmorComparision);
    AddComparingStyle($('#movespeed'), comparisionJson.startingMovespeedComparision);
    AddComparingStyle($('#complexity'), comparisionJson.complexityComparision);
    AddComparingStyle($('#strengthBase'), comparisionJson.strengthBaseComparision);
    AddComparingStyle($('#agilityBase'), comparisionJson.agilityBaseComparision);
    AddComparingStyle($('#intelligenceBase'), comparisionJson.intelligenceBaseComparision);
    AddComparingStyle($('#heraldWinrate'), comparisionJson.weekWinratesComparision.find(w => w.rankBracket === "Herald").winrateComparision);
    AddComparingStyle($('#legendWinrate'), comparisionJson.weekWinratesComparision.find(w => w.rankBracket === "Legend").winrateComparision);
    AddComparingStyle($('#immortalWinrate'), comparisionJson.weekWinratesComparision.find(w => w.rankBracket === "Immortal").winrateComparision);
    comparisionJson.rolesComparision.forEach(role => {AddComparingStyle($(`#${role.name}`), role.levelComparision)});
}

function AddComparingStyle(element, comparisionValue) {
    console.log(element, comparisionValue);
    if (comparisionValue > 0) {
        element.addClass('bg-danger');
    }
    else if (comparisionValue < 0) {
        element.addClass('bg-danger-subtle');
    }
    else {
        element.addClass('bg-success');
    }
}

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
                const hero = response.hero;
                const heroComparision = response.comparision;

                console.log(hero);
                console.log(heroComparision);
                
                const newHeroElement = CreateHeroCard(hero);
                $('.cards-container').prepend(newHeroElement);
                AddComparingStylesToHeroCard(newHeroElement, heroComparision);
            },
            error: function (xhr, status, error) {
                alert('Произошла ошибка при выборе героя.');
            }
        });
    });
});