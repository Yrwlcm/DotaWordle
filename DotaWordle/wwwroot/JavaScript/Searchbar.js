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
                <div class="card-body col-md-4 border border-3 border-danger" style="padding: 0;">
                    <p class="text-center">
                        <strong>Stats:</strong>
                    </p>
                    <ul class="custom-grid-container list-group list-group-flush border border-3 border-danger" style="padding: 0;">
                        <li class="list-group-item" id="attackType">
                            <img src="https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react/icons/${heroData.attackType.toLowerCase()}.svg"
                             style="width: 30px; height: 30px; margin-right: 5px;"/>
                            ${heroData.attackType}
                        </li>
                        <li class="list-group-item" id="attackRange">
                            <img src="https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react//heroes/stats/icon_attack_range.png"
                             style="width: 30px; height: 30px; margin-right: 5px;"/>
                            ${heroData.attackRange}
                        </li>
                        <li class="list-group-item">
                            <img src="https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react//heroes/stats/icon_damage.png"
                             style="width: 30px; height: 30px; margin-right: 5px;"/>
                            <span id="attackDamageMin">${heroData.startingDamageMin}</span> - <span id="attackDamageMax">${heroData.startingDamageMax}</span>
                        </li>
                        <li class="list-group-item" id="armor">
                            <img src="https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react//heroes/stats/icon_armor.png"
                             style="width: 30px; height: 30px; margin-right: 5px;"/>
                            ${Math.round(heroData.startingArmor * 10) / 10}
                        </li>
                        <li class="list-group-item" id="movespeed">
                            <img src="https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react//heroes/stats/icon_movement_speed.png"
                             style="width: 30px; height: 30px; margin-right: 5px;"/>
                            ${heroData.startingMovespeed}
                        </li>
                        <li class="list-group-item" id="complexity">
                            Complexity: ${heroData.complexity}
                        </li>
                        <li class="list-group-item" id="strengthBase">
                            <img src="https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react/icons/hero_strength.png"
                             style="width: 30px; height: 30px; margin-right: 5px;"/>
                            ${heroData.strengthBase}
                        </li>
                        <li class="list-group-item" id="agilityBase">
                            <img src="https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react/icons/hero_agility.png"
                             style="width: 30px; height: 30px; margin-right: 5px;"/>
                            ${heroData.agilityBase}
                        </li>
                        <li class="list-group-item" id="intelligenceBase">
                            <img src="https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react/icons/hero_intelligence.png"
                             style="width: 30px; height: 30px; margin-right: 5px;"/>
                            ${heroData.intelligenceBase}
                        </li>
                        <li class="list-group-item"></li>
                    </ul>
                </div>
                <div class="card-body col-md-1 border border-3 border-danger" style="padding: 0;">
                    <p class="text-center">
                        <strong>Winrates:</strong>
                    </p>
                    <ul class="d-flex flex-column align-items-center justify-content-center list-group list-group-flush">
                        <li id="heraldWinrate" class="list-group-item p-0">
                            <img src="https://cdn.stratz.com/images/dota2/seasonal_rank/medal_1.png" alt="Herald"
                            style="width: 53px; height: 53px; margin-right: 5px;"/>
                            ${heroData.weekWinrates.find(w => w.rankBracket === "Herald").winrate}%
                        </li>
                        <li id="legendWinrate" class="list-group-item p-0">
                            <img src="https://cdn.stratz.com/images/dota2/seasonal_rank/medal_5.png" alt="Legend"
                            style="width: 53px; height: 53px; margin-right: 5px;"/>
                            ${heroData.weekWinrates.find(w => w.rankBracket === "Legend").winrate}%
                        </li>
                        <li id="immortalWinrate" class="list-group-item p-0">
                            <img src="https://cdn.stratz.com/images/dota2/seasonal_rank/medal_8.png" alt="Immortal"
                            style="width: 53px; height: 53px; margin-right: 5px;"/>
                            ${heroData.weekWinrates.find(w => w.rankBracket === "Immortal").winrate}%
                        </li>
                    </ul>
                </div>
                <div class="card-body col-md-4 border border-3 border-danger" style="padding: 0;">
                    <p class="text-center">
                        <strong>Roles:</strong>
                    </p>
                    <ul class="custom-grid-container list-group list-group-flush border border-3 border-danger" style="padding: 0;">
                        ${heroData.roles.map(role => `
                            <li class="list-group-item" id="${role.name}">
                                <p style="margin-bottom: 0">${role.name}</p>
                                <div class="progress" role="progressbar" aria-label="${role.name}" style="height: 10px;"
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
    comparisionJson.rolesComparision.forEach(role => {
        AddComparingStyle($(`#${role.name}`), role.levelComparision)
    });
}

function AddComparingStyle(element, comparisionValue) {
    if (comparisionValue > 0) {
        element.addClass('bg-danger');
    } else if (comparisionValue < 0) {
        element.addClass('bg-danger-subtle');
    } else {
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

        $.ajax({
            url: `api/heroes/compare/hiddenHero/${heroId}`,
            method: 'GET',
            data: {
                __RequestVerificationToken: antiForgeryToken
            },
            success: function (response) {
                const hero = response.hero;
                const heroComparision = response.comparision;

                const newHeroElement = CreateHeroCard(hero);
                $('.cards-container').prepend(newHeroElement);
                // AddComparingStylesToHeroCard(newHeroElement, heroComparision);
            },
            error: function (xhr, status, error) {
                alert('Произошла ошибка при выборе героя.');
            }
        });
    });
});