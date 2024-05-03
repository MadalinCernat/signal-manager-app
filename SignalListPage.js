// SignalListPage.js
import React, { useEffect, useState } from 'react';
import { View, Text, Button } from 'react-native';
import axios from 'axios';

const SignalListPage = ({ navigation }) => {
  const [signals, setSignals] = useState([]);

  useEffect(() => {
    // Fetch currencies from API
    axios.get('YOUR_API_ENDPOINT')
      .then(response => {
        setSignals(response.data);
      })
      .catch(error => {
        console.error('Error fetching signals:', error);
      });
  }, []);

  const handleSignalPress = (signal) => {
    // Navigate to currency details page
    navigation.navigate('SignalDetails', { signal });
  };

  return (
    <View>
      {signals.map(signal => (
        <View key={signal.id}>
          <Text>{signal.name}</Text>
          <Button title="Details" onPress={() => handleSignalPress(signal)} />
        </View>
      ))}
    </View>
  );
};

export default SignalListPage;
