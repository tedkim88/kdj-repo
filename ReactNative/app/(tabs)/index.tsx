import { Link } from "expo-router";
import { Text, View, StyleSheet, TouchableOpacity } from "react-native";
import useTheme from "@/hooks/useTheme";
export default function Index() {
  const { toggleDarkMode } = useTheme();
//just like zustand. instead of getting this from store, I get this from useTheme

  return (
    <View style={styles.container}>
      <Text style={styles.title}>Content testing</Text>
      {/* <Link href="/about">Visit About Screen</Link> */}
      <TouchableOpacity onPress={toggleDarkMode}>
        <Text>Toggle Theme</Text>        
        </TouchableOpacity>
    </View>
  );
}

//react native is file-routing system unlike next.js

const styles = StyleSheet.create({
  container: {
    flex: 1, //min-height: 100vh 같은느낌인듯.. 세로공간 100%차지
    alignItems: "center",
    justifyContent: "center",
  },
  title: {
    fontSize: 20,
    fontWeight: "bold",
  },
  separator: {
    marginVertical: 30,
    height: 1,
    width: "80%",
  },
});
