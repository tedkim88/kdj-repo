import {
  Text,
  View,
  StyleSheet,
  TouchableOpacity,
  StatusBar,
} from "react-native";
import useTheme from "@/hooks/useTheme";
import { useQuery } from "convex/react";
import { api } from "@/convex/_generated/api";
import { ColorScheme } from "@/hooks/useTheme"; // ColorScheme
import { createHomeStyles } from "@/assets/styles/home.styles";
import { SafeAreaView } from "react-native-safe-area-context";
import { LinearGradient } from "expo-linear-gradient";
import Header from "@/components/Header";
// import { useMutation } from "convex/react";
export default function Index() {
  const { toggleDarkMode, colors } = useTheme();
  //just like zustand. instead of getting this from store, I get this from useTheme

  const todos = useQuery(api.todos.getTodos);
  console.log(todos);

  // const addTodo = useMutation(api.todos.addTodo);
  // const clearTodos = useMutation(api.todos.clearAllTodos);

  const homeStyles = createHomeStyles(colors);

  return (
    // <View style={homeStyles.container}>
    <LinearGradient
      colors={colors.gradients.background}
      style={homeStyles.container}
    >
      {/* 그냥View하고 차이..스태터스바 안넘어가도록하는기능인듯 */}
      <StatusBar barStyle={colors.statusBarStyle} />
      <SafeAreaView style={homeStyles.safeArea}>
        {/* 맨위에 시계보이는 가장윗라인 스타일링 */}
        <Header />

        {/* <Text style={homeStyles.title}>Content testing</Text> */}
        {/* <Link href="/about">Visit About Screen</Link> */}
        <TouchableOpacity onPress={toggleDarkMode}>
          <Text>Toggle Theme</Text>
        </TouchableOpacity>
        {/* <TouchableOpacity onPress={() => addTodo({ text: "walk the dog!!" })}>
        <Text>Add a new Todo</Text>
        </TouchableOpacity>
        <TouchableOpacity onPress={() => clearTodos()}>
        <Text>Clear All Todos</Text>
        </TouchableOpacity> */}
      </SafeAreaView>
    </LinearGradient>
  );
}

//react native is file-routing system unlike next.js

// const createStyles = (colors:ColorScheme) => {
//   const styles = StyleSheet.create({
//     container: {
//       flex: 1, //min-height: 100vh 같은느낌인듯.. 세로공간 100%차지
//       alignItems: "center",
//       justifyContent: "center",
//       gap: 10,
//       backgroundColor: colors.bg
//     },
//     title: {
//       fontSize: 20,
//       fontWeight: "bold",
//     },
//     separator: {
//       marginVertical: 30,
//       height: 1,
//       width: "80%",
//     },
//   });

//   return styles;
// };
