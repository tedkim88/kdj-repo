import {
  addWordToList,
  fetchRandomWords,
  Rerendering,
  wordArr,
  getMeaning,
  addWordToList,
} from "./utils";
// import { ShowTotalDef } from "./utils";
// import { renderMeaning } from "./utils";
// import { fetchMeaning } from "./utils";
// import { analytics } from "./firebase";
import {
  getAuth,
  onAuthStateChanged,
  createUserWithEmailAndPassword,
  signInWithEmailAndPassword,
  signOut,
} from "firebase/auth";
import {
  collection,
  addDoc,
  getFirestore,
  doc,
  getDocs,
} from "firebase/firestore";
import { initializeApp } from "firebase/app";
import { getAnalytics } from "firebase/analytics";
import { firebaseConfig } from "./firebase";

let modeChange = document.getElementById("mode-change");
let button = document.querySelector("#add-to-list");
let form = document.querySelector("form");
let list = document.getElementById("list-of-words");
let clearButton = document.getElementById("clear");
document.getElementById("word").focus();
let saveButton = document.getElementById("save");
let isDark = false;

// Firebase 설정 객체
// Firebase 앱 초기화
const app = initializeApp(firebaseConfig);
// 인증 서비스와 Firestore 초기화
const auth = getAuth(app);
const db = getFirestore(app);
// Analytics 초기화 (현재 코드에서는 사용되지 않는 것으로 보입니다)
const analytics = getAnalytics(app);
// 이제 Firestore와 인증 서비스를 사용할 준비가 완료되었습니다.

//to logout any previously logged in id
SignOut();

document.getElementById("sign-up").addEventListener("click", (evt) => {
  evt.preventDefault();
  CreateID();
});

document.getElementById("sign-in").addEventListener("click", (evt) => {
  evt.preventDefault();
  SignIn();
});

document.getElementById("sign-out").addEventListener("click", (evt) => {
  evt.preventDefault();
  SignOut();
});

onAuthStateChanged(auth, (user) => {
  if (user) {
    console.log("User is signed in:", user);
  } else {
    console.log("No user is signed in.");
  }
});

document.querySelector(".board").addEventListener("click", (evt) => {
  evt.preventDefault();
  document.querySelector(".board-section").style.display = "";
  document.querySelector(".py-5").style.display = "none";
  LoadData("questions", "question-list", BoardRendering);
});

function BoardRendering(data) {
  console.log(data);
  const listItem = document.createElement("li");
  listItem.innerHTML = `<strong>${data.subject}</strong>: ${data.content}`;
  document.querySelector("#question-list").appendChild(listItem);
}

document.getElementById('create-account').addEventListener('click',evt=>{
document.querySelector('.signin').style.display = "none";
document.querySelector('.signup').style.display = "";
document.getElementById('create-account').style.display="none";
})



document.querySelector("#back-to-main").addEventListener("click", (evt) => {
  evt.preventDefault();
  document.querySelector(".board-section").style.display = "none";
  document.querySelector(".py-5").style.display = "";
});

//퀴즈기능추가중
document.querySelector("#quiz").addEventListener("click", (evt) => {
  document.querySelectorAll(".content").forEach((item) => {
    item.style.display = "none";
  });

  console.log("hey");
  document.querySelector("#quiz-section").style.display = "";

  LoadData("words", "quiz-render", QuizRendering);
});

document.querySelector(".to-main").addEventListener("click", (evt) => {
  document.querySelector("#quiz-section").style.display = "none";

  document.querySelectorAll(".content").forEach((item) => {
    item.style.display = "";
  });
});

function QuizRendering(data) {
  console.log(data + 'hellowervwrvwervwevrwevrwevr');
    const quizRenderArr = data.map((item) => {
    const ol = document.createElement("ol");
    // ol.setAttribute("style", "display:none");
    // Loop through the definitions and create <li> elements
    item.definitions.forEach((definition) => {
      let list = document.createElement("li");
      list.textContent = definition;
      ol.append(list);
    });
    ol.classList.add("hidden");

    // Return the HTML structure with the ol list as the content
    return `
    <div class="card" style="width: 18rem;">
      <div class="card-body">
        <h5 class="card-title">${item.word}</h5>
        <p class="card-text">${ol.outerHTML}</p>
        <a href="#" class="btn btn-primary answer-btn">Check Answer</a>
      </div>
    </div>
  `;
  });
  
  document.querySelector("#quiz-render").innerHTML += quizRenderArr;
}

document.querySelector("#quiz-render").addEventListener("click", (e) => {
  console.log("123123123213");
  if (e.target.tagName === "A") {
    console.log("here");
    console.log(e.target.parentNode);
    // <a> 태그의 부모 요소에서 <p> 태그를 찾기
    let ol = e.target.parentNode.querySelector("ol");

    // <p> 태그가 숨겨져 있으면 보이게 하고, 보이면 숨기기
    if (ol.classList.contains("hidden")) {
      console.log("123");
      ol.classList.remove("hidden"); // 기본 스타일로 보이게 설정
      e.target.textContent = "Hide";
    } else {
      console.log("456");
      ol.classList.add("hidden"); // 숨기기
      e.target.textContent = "Check Answer";
    }
  }
});

let questionForm = document.querySelector(".question-form");
questionForm.addEventListener("submit", (evt) => {
  evt.preventDefault();

  let title = questionForm.elements["subject"].value;
  let text = questionForm.elements["question"].value;

  let collection = "questions";
  let input = { subject: title, content: text };
  addToDB(collection, input);
});

// db가 이미 Firestore 인스턴스라고 가정
// document.getElementById("firebase-save").addEventListener("click", () => {
//   addToDB(); // 함수 호출
// });

document.getElementById("fetch-random").addEventListener("click", (evt) => {
  evt.preventDefault();
  fetchRandomWords();
});

// add to list
button.addEventListener("click", (evt) => {
  evt.preventDefault();
  let wordValue = document.getElementById("word").value;
  addWordToList(wordValue, getMeaning(wordValue));
});

// Clear list
clearButton.addEventListener("click", (evt) => {
  evt.preventDefault();
  console.log("hello");
  list.innerHTML = "";
  wordArr.length = 0;
  console.log(wordArr);
  document.getElementById("word").value = "";
});

saveButton.addEventListener("click", (evt) => {
  if (wordArr.length === 0) {
    alert("Save Fail. The list is empty.");
    return;
  }
  const jsonData = JSON.stringify(wordArr, null, 2);
  // console.log(`${jsonData} hello`);
  const blob = new Blob([jsonData], { type: "application/json" });
  const link = document.createElement("a"); // 링크 요소 생성
  link.href = URL.createObjectURL(blob); // Blob을 URL로 변환
  link.download = "data.json"; // 다운로드할 파일 이름 설정
  link.click(); // 다운로드 실행

  console.log(jsonData);
  let result = JSON.parse(jsonData);

  //여기서부턴 데이터베이스에저장 파이어베이스
  const extracted = []; // Initialize as an empty array

  result.forEach((item) => {
    const wordObject = {
      word: item.word,
      definitions: [],
    };

    item.meaning.forEach((each) => {
      each.forEach((def) => {
        wordObject.definitions.push(def.definition);
      });
    });

    extracted.push(wordObject);
  });

  console.log(extracted); //보기좋게 단어 / 뜻의 배열로 extracted 했음. //이거 퀴즈로 쓸 예정 붓스트랩card랑 결합하여여

  addToDB("words", { extracted }); // 저장(콜렉션이름(테이블이름),객체로만 저장됨 firebase저장특성.이거 로딩할 때 잘 분리해야함)
});

// Add word to list

// Rerender the list

// Delete word using event delegation
list.addEventListener("click", (event) => {
  if (event.target.classList.contains("delete-btn")) {
    const indexToDelete = event.target.dataset.index; // 클릭한 버튼의 인덱스 가져오기
    wordArr.splice(indexToDelete, 1); // 배열에서 항목 삭제
    Rerendering(wordArr); // 리스트 다시 렌더링
  }
});

document.getElementById("clear-button").addEventListener("click", () => {
  document.querySelector("#top-ten-words").innerHTML = "";
  document.querySelector(".meaning-box").innerHTML = "";
});

document.querySelector("#top-ten-words").addEventListener("click", (event) => {
  // 클릭한 요소가 버튼인 경우
  if (
    event.target.tagName === "BUTTON" &&
    event.target.textContent == "Meaning"
  ) {
    console.log(event.target.parentNode.querySelector("span"));
    let targetWord = event.target.parentNode.querySelector("span").textContent;
    getMeaning(targetWord);
  }
});

document.querySelector("#list-of-words").addEventListener("click", (event) => {
  // 클릭한 요소가 버튼인 경우
  if (
    event.target.tagName === "BUTTON" &&
    event.target.textContent == "Meaning"
  ) {
    let targetWord = event.target.parentNode.querySelector("span").textContent;
    getMeaning(targetWord);
  }
});

function SignIn() {
  let email = document.getElementById("signin-id").value;
  let password = document.getElementById("signin-password").value;

  signInWithEmailAndPassword(auth, email, password)
    .then((userCredential) => {
      // Signed in
      const user = userCredential.user;
      console.log("logged-in!", user);
      // ...
      document.querySelectorAll(".content").forEach((item) => {
        item.style.display = "";
      });

      document.querySelector(".signup").style.display = "none";
      document.querySelector(".signin").style.display = "none";
      document.getElementById("sign-out").style.display = "";
    })
    .catch((error) => {
      const errorCode = error.code;
      const errorMessage = error.message;
      alert(errorMessage);
    });
}

function CreateID() {
  let email = document.getElementById("create-id").value;
  let password = document.getElementById("create-password").value;

  createUserWithEmailAndPassword(auth, email, password)
    .then((userCredential) => {
      // Signed up
      const user = userCredential.user;
      // ...
      
      document.querySelector('.signin').style.display = "";
      document.querySelector('.signup').style.display = "none";
      document.getElementById('create-account').style.display="";
      alert('Your account has been successfully made.');
      document.getElementById("create-id").value = null;
      document.getElementById("create-password").value = null;

      console.log("hi");
      console.log(user);
    })
    .catch((error) => {
      const errorCode = error.code;
      const errorMessage = error.message;
      alert(errorMessage);
      // ..
    });
}

function SignOut() {
  signOut(auth)
    .then(() => {
      console.log("signed out!");
      // Sign-out successful.
      document.querySelectorAll(".content").forEach((item) => {
        item.style.display = "none";
      });
      document.getElementById("sign-out").style.display = "none";
      // document.querySelector(".signup").style.display = "";
      document.querySelector(".signin").style.display = "";
    })
    .catch((error) => {});
}

async function addToDB(collectionName, input) {
  const user = auth.currentUser;
  if (user) {
    try {
      const docRef = await addDoc(collection(db, collectionName), input);
      console.log("Document written with ID: ", docRef.id);
    } catch (e) {
      console.error("Error adding document: ", e);
    }
  } else {
    console.log("User is not authenticated, cannot add data.");
    alert("User is not authenticated, cannot add data. Failed to add data.");
  }
}

////////////////////////////////////////////////////loading from database

async function LoadData(collectionName, renderElement, callback) {
  // "questions" 컬렉션의 모든 문서를 가져옴
  console.log("werwerwer");
  const querySnapshot = await getDocs(collection(db, collectionName));
  console.log(querySnapshot);
  // 데이터를 렌더링할 HTML 요소 선택
  console.log(renderElement);
  document.getElementById(renderElement).innerHTML = "";
  // 기존 내용을 초기화

  // 문서 데이터를 순회하며 렌더링
  let emptyArr = [];
  querySnapshot.forEach((doc) => {
    const data = doc.data();
    console.log(data);
    data.extracted.forEach(item=>{
      emptyArr.push(item);
      })
    // const listItem = document.createElement("li");
    // listItem.innerHTML = `<strong>${data.subject}</strong>: ${data.content}`;
    // questionList.appendChild(listItem);
  });
  console.log(emptyArr);
  let seenWords = new Set();
let uniqueArray = emptyArr.filter(item => {
  if (seenWords.has(item.word)) {
    return false;
  } else {
    seenWords.add(item.word);
    return true;
  }
});
console.log(uniqueArray);
  callback(uniqueArray);
}

modeChange.addEventListener("click", () => {
  if (!isDark) {
    // 다크모드 적용
    document.querySelector(".py-5").style.backgroundColor = "black";
    document.querySelector(".py-5").style.color = "white"; // 텍스트 색 변경
    document.body.style.backgroundColor = "#121212"; // 바디 배경색
    document.body.style.color = "white"; // 바디 텍스트 색

    // 추가 스타일 설정
    isDark = true;
  } else {
    // 화이트모드 적용
    document.querySelector(".py-5").style.backgroundColor = "white";
    document.querySelector(".py-5").style.color = "black"; // 텍스트 색 변경
    document.body.style.backgroundColor = "white"; // 바디 배경색
    document.body.style.color = "black"; // 바디 텍스트 색

    // 추가 스타일 설정
    isDark = false;
  }
});
