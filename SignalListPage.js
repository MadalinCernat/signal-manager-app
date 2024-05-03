import React, { useEffect, useState } from 'react';
import { View, Text, Button } from 'react-native';

const SignalListPage = ({ navigation }) => {
  const [signals, setSignals] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch('https://localhost:7174/api/Signal');
        if (!response.ok) {
          throw new Error('Failed to fetch signals');
        }
        const data = await response.json();
        setSignals(data);
      } catch (error) {
        console.error('Error fetching signals:', error);
      }
    };

    fetchData();
  }, []);

  const handleSignalPress = (signal) => {
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
