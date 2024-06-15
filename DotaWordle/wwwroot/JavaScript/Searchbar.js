function CreateHeroCard(heroData) {
    return `
        <div class="card card-custom hero-card">
            <div class="row g-0 mx-5 my-3 text-center flex-row align-items-center justify-content-center ">
                <div class="col-md-3 d-flex align-items-center justify-content-center">
                    <img src="Images/Heroes/${heroData.name}.png"
                         class="img-fluid rounded-start" alt="${heroData.name}">
                </div>
                <div class="col-md-1 d-flex align-items-center justify-content-center">
                    <img src="Images/Attributes/${heroData.primaryAttributeName}.png"
                         class="img-fluid rounded-start" alt="${heroData.primaryAttributeName}"
                         style="width: 60px;">
                         
                     <img id="primaryAttributeStatus" src="Images/ComparingSymbols/cross.png" style="width: 25px; height: 25px; margin-left: 10px;">
                </div>
                <div class="col-md-5 hero-name">
                    <p class="card-title">${heroData.name}</p>
                </div>
            </div>
            <div class="row g-0 p-3">
                <div class="card-body col-md-4 border-start border-end" style="padding: 0;">
                    <p class="text-center">
                        <strong>Stats:</strong>
                    </p>
                    <ul class="custom-grid-container list-group list-group-flush " style="padding: 0;">
                        <li class="list-group-item" >
                            <img src="https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react/icons/${heroData.attackType.toLowerCase()}.svg"
                             style="width: 30px; height: 30px; margin-right: 5px;"/>
                            ${heroData.attackType}
                            <img id="attackTypeStatus" src="Images/ComparingSymbols/cross.png" style="width: 15px; height: 15px; margin-left: 8px;">
                        </li>
                        <li class="list-group-item" >
                            <img src="https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react//heroes/stats/icon_attack_range.png"
                             style="width: 30px; height: 30px; margin-right: 5px;"/>
                            ${heroData.attackRange}
                            <img id="attackRangeStatus" src="Images/ComparingSymbols/cross.png" style="width: 15px; height: 15px; margin-left: 8px;">
                        </li>
                        <li class="list-group-item">
                            <img src="https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react//heroes/stats/icon_damage.png"
                             style="width: 30px; height: 30px; margin-right: 5px;"/>
                            <img id="attackDamageMinStatus" src="Images/ComparingSymbols/cross.png" style="width: 15px; height: 15px; margin-right: 8px;"><span >${heroData.damageMinBase}</span>
                             - <span>${heroData.damageMaxBase}</span><img id="attackDamageMaxStatus" src="Images/ComparingSymbols/cross.png" style="width: 15px; height: 15px; margin-left: 8px;">
                        </li>
                        <li class="list-group-item">
                            <img src="https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react//heroes/stats/icon_armor.png"
                             style="width: 30px; height: 30px; margin-right: 5px;"/>
                            ${Math.round(heroData.armorBase * 10) / 10}
                            <img id="armorStatus" src="Images/ComparingSymbols/cross.png" style="width: 15px; height: 15px; margin-left: 8px;">
                        </li>
                        <li class="list-group-item">
                            <img src="https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react//heroes/stats/icon_movement_speed.png"
                             style="width: 30px; height: 30px; margin-right: 5px;"/>
                            ${heroData.moveSpeedBase}
                            <img id="movespeedStatus" src="Images/ComparingSymbols/cross.png" style="width: 15px; height: 15px; margin-left: 8px;">
                        </li>
                        <li class="list-group-item" >
                            Complexity: ${heroData.complexity}
                            <img id="complexityStatus" src="Images/ComparingSymbols/cross.png" style="width: 15px; height: 15px; margin-left: 8px;">
                        </li>
                        <li class="list-group-item" >
                            <img src="Images/Attributes/Strength.png"
                             style="width: 30px; height: 30px; margin-right: 5px;"/>
                            ${heroData.strengthBase}
                            <img id="strengthBaseStatus" src="Images/ComparingSymbols/cross.png" style="width: 15px; height: 15px; margin-left: 8px;">
                        </li>
                        <li class="list-group-item" >
                            <img src="Images/Attributes/Agility.png"
                             style="width: 30px; height: 30px; margin-right: 5px;"/>
                            ${heroData.agilityBase}
                            <img id="agilityBaseStatus" src="Images/ComparingSymbols/cross.png" style="width: 15px; height: 15px; margin-left: 8px;">
                        </li>
                        <li class="list-group-item" >
                            <img src="Images/Attributes/Intelligence.png"
                             style="width: 30px; height: 30px; margin-right: 5px;"/>
                            ${heroData.intelligenceBase}
                            <img id="intelligenceBaseStatus" src="Images/ComparingSymbols/cross.png" style="width: 15px; height: 15px; margin-left: 8px;">
                        </li>
                        <li class="list-group-item"></li>
                    </ul>
                </div>
                <div class="card-body col-md-1 border-start border-end" style="padding: 0;">
                    <p class="text-center">
                        <strong>Winrates:</strong>
                    </p>
                    <ul class="d-flex flex-column align-items-center justify-content-center list-group list-group-flush">
                        <li class="list-group-item p-0">
                            <img src="https://cdn.stratz.com/images/dota2/seasonal_rank/medal_1.png" alt="Herald"
                            style="width: 53px; height: 53px; margin-right: 5px;"/>
                            ${heroData.weekWinrates.find(w => w.rankBracket === "Herald").winrate}%
                            <img id="heraldWinrateStatus" src="Images/ComparingSymbols/cross.png" style="width: 15px; height: 15px; margin-left: 8px;">
                        </li>
                        <li class="list-group-item p-0">
                            <img src="https://cdn.stratz.com/images/dota2/seasonal_rank/medal_5.png" alt="Legend"
                            style="width: 53px; height: 53px; margin-right: 5px;"/>
                            ${heroData.weekWinrates.find(w => w.rankBracket === "Legend").winrate}%
                            <img id="legendWinrateStatus" src="Images/ComparingSymbols/cross.png" style="width: 15px; height: 15px; margin-left: 8px;">
                        </li>
                        <li class="list-group-item p-0">
                            <img src="https://cdn.stratz.com/images/dota2/seasonal_rank/medal_8.png" alt="Immortal"
                            style="width: 53px; height: 53px; margin-right: 5px;"/>
                            ${heroData.weekWinrates.find(w => w.rankBracket === "Immortal").winrate}%
                            <img id="immortalWinrateStatus" src="Images/ComparingSymbols/cross.png" style="width: 15px; height: 15px; margin-left: 8px;">
                        </li>
                    </ul>
                </div>
                <div class="card-body col-md-4 border-start border-end" style="padding: 0;">
                    <p class="text-center">
                        <strong>Roles:</strong>
                    </p>
                    <ul class="custom-grid-container list-group list-group-flush " style="padding: 0;">
                        ${heroData.roles.map(role => `
                            <li class="list-group-item">
                                <p style="margin-bottom: 0">${role.name}</p>
                                <div class="progress" role="progressbar" aria-label="${role.name}" style="height: 10px;"
                                     aria-valuenow="${Math.round(role.level * 33.33)}" aria-valuemin="0" aria-valuemax="3">
                                    <div id="${role.name}Status" class="progress-bar" style="width: ${Math.round(role.level * 33.33)}%"></div>
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
    AddComparingStyleToBool($('#primaryAttributeStatus'), comparisionJson.samePrimaryAttributeName);
    AddComparingStyleToBool($('#attackTypeStatus'), comparisionJson.sameAttackType);
    AddComparingStyleToNumeric($('#attackRangeStatus'), comparisionJson.attackRangeComparision);
    AddComparingStyleToNumeric($('#attackDamageMinStatus'), comparisionJson.startingDamageMinComparision);
    AddComparingStyleToNumeric($('#attackDamageMaxStatus'), comparisionJson.startingDamageMaxComparision);
    AddComparingStyleToNumeric($('#armorStatus'), comparisionJson.startingArmorComparision);
    AddComparingStyleToNumeric($('#movespeedStatus'), comparisionJson.startingMovespeedComparision);
    AddComparingStyleToNumeric($('#complexityStatus'), comparisionJson.complexityComparision);
    AddComparingStyleToNumeric($('#strengthBaseStatus'), comparisionJson.strengthBaseComparision);
    AddComparingStyleToNumeric($('#agilityBaseStatus'), comparisionJson.agilityBaseComparision);
    AddComparingStyleToNumeric($('#intelligenceBaseStatus'), comparisionJson.intelligenceBaseComparision);
    AddComparingStyleToNumeric($('#heraldWinrateStatus'), comparisionJson.weekWinratesComparision.find(w => w.rankBracket === "Herald").winrateComparision);
    AddComparingStyleToNumeric($('#legendWinrateStatus'), comparisionJson.weekWinratesComparision.find(w => w.rankBracket === "Legend").winrateComparision);
    AddComparingStyleToNumeric($('#immortalWinrateStatus'), comparisionJson.weekWinratesComparision.find(w => w.rankBracket === "Immortal").winrateComparision);
    comparisionJson.rolesComparision.forEach(role => {
        AddComparingStyleToProgress($(`#${role.name}Status`), role.levelComparision)
    });
}

function AddComparingStyleToProgress(element, comparisionValue) {
    if (comparisionValue !== 0) {
        element.addClass('bg-danger');
    } else {
        element.addClass('bg-success');
    }
}

function AddComparingStyleToBool(element, comparisionValue) {
    if (comparisionValue === true) {
        element.attr('src', 'Images/ComparingSymbols/check.png');
    } else {
        element.attr('src', 'Images/ComparingSymbols/cross.png');
    }
}

function AddComparingStyleToNumeric(element, comparisionValue) {
    if (comparisionValue < 0) {
        element.attr('src', 'Images/ComparingSymbols/down.png');
    } else if (comparisionValue > 0) {
        element.attr('src', 'Images/ComparingSymbols/up.png');
    } else {
        element.attr('src', 'Images/ComparingSymbols/check.png');
    }
}

function FilterHeroes(heroes, filter, hiddenHeroesIds) {
    let resultHeroes = heroes;

    if (filter) {
        resultHeroes = resultHeroes.filter(function () {
            return $(this).find('span').text().toLowerCase().indexOf(filter) > -1;
        });
    }

    resultHeroes = resultHeroes.filter(function () {
        return !hiddenHeroesIds.includes($(this).data('id'));
    });

    return resultHeroes;
}

function DisplayHeroes(results, items, filter, hiddenHeroes) {
    let heroes = FilterHeroes(items, filter, hiddenHeroes);
    items.hide();
    heroes.show();
    results.show();
}


$(document).ready(function () {
    const results = $('#results');
    const items = results.find('.result-item');
    let heroesChosen = 0;
    let hiddenHeroes = [];

    // Показать всех героев при фокусе на поле ввода, если поле пустое
    $('#search-input').on('focus', function () {
        const filter = $(this).val().toLowerCase();
        DisplayHeroes(results, items, filter, hiddenHeroes);
    });

    // Скрыть результаты при клике вне области поиска
    $(document).on('click', function (e) {
        if (!$(e.target).closest('.search-container').length) {
            results.hide();
        }
    });

    // Фильтрация героев при вводе текста
    $('#search-input').on('input', function () {
        const filter = $(this).val().toLowerCase();
        DisplayHeroes(results, items, filter, hiddenHeroes);
    });

    // Post запрос для сравнения героев
    items.on('click', function () {
        const heroId = $(this).data('id');
        const antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();
        hiddenHeroes.push(heroId);

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

                heroesChosen++;
                $('.clicks-count').text(heroesChosen);

                if (heroComparision.comparedHeroName === heroComparision.heroName) {
                    $('#successModal').modal('show');
                }
            },
            error: function (xhr, status, error) {
                alert('Произошла ошибка при выборе героя.');
            }
        });

        DisplayHeroes(results, items, '', hiddenHeroes);
    });
});