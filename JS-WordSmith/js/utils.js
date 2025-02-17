let wordArr = [];
let list = document.getElementById('list-of-words');


const addWordToList = (value, meaning) => {
    if (isEmpty(value)) {
        alert('Please enter a word');
        return;
    }

    if (overlapping(value)) {
        alert('You have that word already on the list.');
        return;
    }

    meaning.then(data => {
        let meaningArr = [];
        data.forEach(item => {
            console.log(item)
            item.meanings.forEach(line => meaningArr.push(line.definitions))
        })

        wordArr.push({ word: value, meaning: meaningArr }); // 데이터 추가
        Rerendering(wordArr); // 데이터를 추가한 후 렌더링
        console.log(wordArr)

    }).catch(error => {
        console.error("Error fetching meaning:", error); // 오류 처리
    });

    // 새 단어가 추가된 후 바로 렌더링
};




async function fetchRandomWords() {
    document.querySelector('#top-ten-words').innerHTML = "";
    let wordCount = document.querySelector('#wordCount').value;
    if (wordCount <= 0 || wordCount > 10) {
        alert('Input should be between 1 and 10.');
        return;
    }
    const response = await fetch(`https://random-word-api.vercel.app/api?words=${wordCount}`);
    const data = await response.json();
    // console.log(data);

    data.forEach(item => {

        let list = document.createElement('li');
        list.setAttribute('style', 'margin-bottom:10px;');
        list.innerHTML = `<span>${item}</span>`;
        let addButton = document.createElement('button');
        addButton.textContent = "Add to the List"
        addButton.style = 'margin-left:10px;';

        let meaningButton = document.createElement('button');
        meaningButton.textContent = "Meaning"
        meaningButton.style = 'margin-left:10px;';


        list.appendChild(addButton);
        list.appendChild(meaningButton);
        addButton.classList.add('btn', 'btn-primary', 'btn-sm');
        // console.log('hello');
        document.querySelector('#top-ten-words').appendChild(list);

        addButton.addEventListener('click', (evt) => {
            console.log('hello')
            console.log(evt.target.parentNode);
            let wordValue = evt.target.parentNode.querySelector('span').innerText;
         
            if (overlapping(wordValue)) {
               
                console.log(wordArr)
                alert('there is an overlapping word already.')
                return;
            }
            let meaningArr = [];
            let result = getMeaning(wordValue)

            result.then(each => {
                each.forEach(item => {
                    item.meanings.forEach(line => meaningArr.push(line.definitions))
                })

                wordArr.push({ word: wordValue, meaning: meaningArr });
                Rerendering(wordArr);




            })

        })
    })
}


function Rerendering(editedArr) {
    list.innerHTML = ""; // 기존 목록 지우기

    editedArr.forEach((item, index) => {

        let listElement = document.createElement('li');
        listElement.setAttribute('style', 'margin-bottom:10px;');
        listElement.innerHTML = `<span>${item.word}</span>`;
        // listElement.innerText += 'hello';

        let buttonElement = document.createElement('button');
        buttonElement.textContent = 'X';
        buttonElement.dataset.index = index;
        buttonElement.classList.add('delete-btn');

        let meaningButton = document.createElement('button');
        meaningButton.textContent = "Meaning"
        meaningButton.style = 'margin-left:10px;';

        listElement.appendChild(meaningButton);
        listElement.appendChild(buttonElement);
        list.appendChild(listElement);
        // console.log(wordArr);
    });
}




async function getMeaning(word) {
    let meaningBox = document.querySelector('.meaning-box');
    // meaningBox.querySelectorAll('p').forEach(pTag => {
    //     pTag.innerHTML = "";
    // })
    meaningBox.innerHTML = "";
    let headElement = document.createElement('h3');
    headElement.textContent = 'Meaning Area';
    meaningBox.append(headElement);

    const result = await fetchMeaning(word);

    if (result === undefined) {

        return
    }
    result.forEach((item, index) => {

        let word = document.createElement('p');
        let partOfSpeech = document.createElement('p');
        let definition = document.createElement('p');
        let sourceURL = document.createElement('p');
        let click = document.createElement('a');
        click.textContent = 'Click for source url(wiki)';
        sourceURL.append(click);
        let hrLine = document.createElement('hr');
        word.textContent = `${index + 1})Word: ${item.word}`;
        partOfSpeech.textContent = `Part of Speech: ${item.meanings[0].partOfSpeech}`;
        definition.textContent = `Definition: ${item.meanings[0].definitions[0].definition}`;
        click.setAttribute('href', item.sourceUrls[0]);
        click.setAttribute('target', '_blank');
        let div = document.createElement('div');
        div.append(word, partOfSpeech, definition, sourceURL, hrLine)

        // renderMeaning(word, partOfSpeech, definition, sourceURL, hrLine);
        renderMeaning(div);

    })

    let totalMeaning = document.createElement('a');
    totalMeaning.textContent = "Click for total meaning"
    totalMeaning.setAttribute('href', '#')


    let meaningArr = [];
    result.forEach(item => {

        item.meanings.forEach(line => meaningArr.push(line.definitions))
    })
    //여기부터 다시시작하면됨. 이거 토탈미닝클릭하면 리스팅하는걸로


    totalMeaning.addEventListener('click', (evt) => {
        evt.preventDefault();
        ShowTotalDef(meaningArr);
    })


    meaningBox.append(totalMeaning);

    return result;
}




function ShowTotalDef(totalMeaning) {
    let meaningBox = document.querySelector('.meaning-box');

    let existingUl = meaningBox.querySelector('ul');


    if (existingUl) {
        if (existingUl.style.display === 'none') {
            existingUl.style.display = ''; // 다시 표시
        } else {
            existingUl.style.display = 'none'; // 숨기기
        }
        return;
    }

    let listOfDefinitions = document.createElement('ul');
    totalMeaning.forEach(item => {
        item.forEach(each => {
            let li = document.createElement('li');
            li.textContent = each.definition;
            listOfDefinitions.append(li)
        })
    })

    meaningBox.append(listOfDefinitions);

}



function renderMeaning(wordDiv) {
    let meaningBox = document.querySelector('.meaning-box');

    meaningBox.append(wordDiv);
};


async function fetchMeaning(word) {
    const response = await fetch(`https://api.dictionaryapi.dev/api/v2/entries/en/${word}`);
    if (!response.ok) {
        alert('We could not find data.');
        return;
    }
    else {
        const data = await response.json();
        return data;
    }
}


function isEmpty(string) {
    return string.trim() === "";
}

function overlapping(string) {
    return wordArr.find(item => item.word.trim() === string.trim()) !== undefined;
}



export { getMeaning }
export { ShowTotalDef }
export { renderMeaning }
export { fetchMeaning }
export { addWordToList }
export { fetchRandomWords }
export { Rerendering }
export { wordArr }
