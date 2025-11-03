import { Link } from "expo-router";
import { Text, View, StyleSheet } from "react-native";

export default function Index() {
  return (
    <View style={styles.container}>
      <Text style={styles.title}>Content testing</Text>
      {/* <Link href="/about">Visit About Screen</Link> */}
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
