// This object represent the waveform generator
var WaveformGenerator = {
    // The generateWaveform function takes 4 parameters:
    //     - type, the type of waveform to be generated
    //     - frequency, the frequency of the waveform to be generated
    //     - amp, the maximum amplitude of the waveform to be generated
    //     - duration, the length (in seconds) of the waveform to be generated
    generateWaveform: function(type, frequency, amp, duration) {
        var nyquistFrequency = sampleRate / 2; // Nyquist frequency
        var totalSamples = Math.floor(sampleRate * duration); // Number of samples to generate
        var result = []; // The temporary array for storing the generated samples

        switch(type) {
            case "sine-time": // Sine wave, time domain
                for (var i = 0; i < totalSamples; ++i) {
                    var currentTime = i / sampleRate;
                    result.push(amp * Math.sin(2.0 * Math.PI * frequency * currentTime));
                }
                break;

            case "square-time": // Square wave, time domain
                /**
                * TODO: Complete this generator
                **/
                var oneCycle = sampleRate / frequency;
                var halfCycle = oneCycle / 2;
                for (var i = 0; i < totalSamples; i++){
                    var pos = i % parseInt(oneCycle);
                    if(pos < halfCycle)
                        result.push(amp * 1);
                    else
                        result.push(amp * -1);
                }
                break;

            case "square-additive": // Square wave, additive synthesis
                /**
                * TODO: Complete this generator
                **/
                for(var i = 0; i < totalSamples; i++){
                    var t = i / sampleRate;
                    var sample = 0;
                    for(var k = 1; (k * frequency < nyquistFrequency) && (k <= 250 * 2); k += 2){
                        sample += (1.0/k) * Math.sin(2 * Math.PI * k * frequency * t);
                    }
                    result.push((4 / Math.PI) * amp * sample);
                }
                break;

            case "sawtooth-time": // Sawtooth wave, time domain
                /**
                * TODO: Complete this generator
                **/
                var oneCycle = sampleRate / frequency;
                for (var i = 0; i < totalSamples; i++){
                    var pos = i % parseInt(oneCycle);
                    var fraction = pos / oneCycle;
                    result.push(amp * (2 * (1.0 - fraction) - 1));
                }
                break;

            case "sawtooth-additive": // Sawtooth wave, additive synthesis
                /**
                * TODO: Complete this generator
                **/
               for(var i = 0; i < totalSamples; i++){
                   var t = i / sampleRate;
                   var sample = 0;
                   for(var k = 1; (k * frequency < nyquistFrequency) && (k <= 250); k++){
                       sample += (1.0/k) * Math.sin(2 * Math.PI * k * frequency * t);
                   }
                   result.push((2 / Math.PI) * amp * sample);
               }
                break;

            case "triangle-additive": // Triangle wave, additive synthesis
                /**
                * TODO: Complete this generator
                **/
                var basis = $("#triangle-additive-basis>option:selected").val();
                for(var i = 0; i < totalSamples; i++){
                    var t = i / sampleRate;
                    var sample = 0;
                    for(var k = 1; (k * frequency < nyquistFrequency) && (k <= 250 * 2); k += 2){
                        if(basis == "cosine")
                            sample += (1.0 / (k * k)) * Math.cos(2 * Math.PI * k * frequency * t);
                        else if(basis == "sine"){
                            if(k % 4 == 1)
                                sample += (1.0 / (k * k)) * Math.sin(2 * Math.PI * k * frequency * t);
                            else
                                sample -= (1.0 / (k * k)) * Math.sin(2 * Math.PI * k * frequency * t);
                        }
                    }
                    result.push((8 / (Math.PI * Math.PI)) * amp * sample);
                }
                break;

            case "karplus-strong": // Karplus-Strong algorithm
                /**
                * TODO: Complete this generator
                **/

                // Obtain all the required parameters
                var base = $("#karplus-base>option:selected").val();
                var b = parseFloat($("#karplus-b").val());
                var delay = parseInt($("#karplus-p").val());

                var kuseFreq = $("#karplus-use-freq").prop("checked");
                if(kuseFreq){
                    delay = sampleRate / frequency;
                    delay = parseInt(delay);
                }
                for(var i = 0; i < totalSamples; i++){
                    if(i < delay){
                        if(base == "white-noise"){
                            result.push(amp * (Math.random() * 2 - 1));
                        }
                        else if(base == "sawtooth"){
                            var pos = i % delay;
                            var fraction = pos / delay;
                            result.push(amp * (2 * (1.0 - fraction) - 1));
                        }
                    }
                    else{
                        if(i - delay - 1 >= 0){
                            sample = 0.5 * (result[i - delay] + result[i - delay - 1]);
                        }
                        else{
                            sample = 0.5 * result[i - delay];
                        }

                        if(b == 1){
                            result.push(sample);
                        }
                        else if(b == 0){
                            result.push(-1 * sample);
                        }
                        else{
                            var random = Math.random();
                            if(random < b)
                                result.push(sample);
                            else
                                result.push(-1 * sample);
                        }
                    }
                }
                

                break;

            case "white-noise": // White noise
                /**
                * TODO: Complete this generator
                **/
                for(var i = 0; i < totalSamples; i++){
                    result.push(amp * (Math.random() * 2 - 1));
                }
                break;

            case "customized-additive-synthesis": // Customized additive synthesis
                /**
                * TODO: Complete this generator
                **/

                // Obtain all the required parameters
				var harmonics = [];
				for (var h = 1; h <= 10; ++h) {
					harmonics.push($("#additive-f" + h).val());
				}
                var max = 0, min = 0;
                for(var i = 0; i < totalSamples; i++){
                    var t = i / sampleRate;
                    var sample = 0;
                    for(var k = 1; (k < 10) && (k * frequency < nyquistFrequency); k++){
                        sample += harmonics[k - 1] * Math.sin(2 * Math.PI * k * frequency * t);
                    }
                    if(max < sample)
                        max = sample;
                    if(min > sample)
                        min = sample;
                    result.push(amp * sample);
                }
                min = -1 * min;
                var biggest = Math.max(max, min);
                var multiplier = 1 / biggest;
                for(var i = 0; i < totalSamples; i++){
                    result[i] = result[i] * multiplier;
                }
                break;

            case "fm": // FM
                /**
                * TODO: Complete this generator
                **/

                // Obtain all the required parameters
                var carrierFrequency = parseFloat($("#fm-carrier-frequency").val());
                var carrierAmplitude = parseFloat($("#fm-carrier-amplitude").val());
                var modulationFrequency = parseFloat($("#fm-modulation-frequency").val());
                var modulationAmplitude = parseFloat($("#fm-modulation-amplitude").val());
                var useADSR = $("#fm-use-adsr").prop("checked");
                if(useADSR) { // Obtain the ADSR parameters
                    var attackDuration = parseFloat($("#fm-adsr-attack-duration").val()) * sampleRate;
                    var decayDuration = parseFloat($("#fm-adsr-decay-duration").val()) * sampleRate;
                    var releaseDuration = parseFloat($("#fm-adsr-release-duration").val()) * sampleRate;
                    var sustainLevel = parseFloat($("#fm-adsr-sustain-level").val()) / 100.0;
                }

                var useFreq = $("#fm-use-freq-multiplier").prop("checked");
                if(useFreq == true){
                    carrierFrequency *= frequency;
                    modulationFrequency *= frequency;
                }

                for (var i = 0; i < totalSamples; ++i) {
                    var t = i / sampleRate;
                    var modulator = modulationAmplitude * Math.sin(2 * Math.PI * modulationFrequency * t);
                    if(useADSR){
                        if(i < attackDuration){
                            modulator *= lerp(0.0, 1.0, (i + 1) / attackDuration);
                        }
                        else if(i < attackDuration + decayDuration){
                            modulator *= lerp(1.0, sustainLevel, (i - attackDuration + 1) / decayDuration);
                        }
                        else if(i < totalSamples - releaseDuration){
                            modulator *= sustainLevel;
                        }
                        else{
                            modulator *= lerp(sustainLevel, 0.0, (i - (totalSamples - releaseDuration) + 1) / releaseDuration);
                        }
                    }
                    sample = carrierAmplitude * Math.sin(2.0 * Math.PI * carrierFrequency * t + modulator);
                    result.push(amp * sample);
                }

                break;

            case "repeating-narrow-pulse": // Repeating narrow pulse
                var cycle = Math.floor(sampleRate / frequency);
                for (var i = 0; i < totalSamples; ++i) {
                    if(i % cycle === 0) {
                        result.push(amp * 1.0);
                    } else if(i % cycle === 1) {
                        result.push(amp * -1.0);
                    } else {
                        result.push(0.0);
                    }
                }
                break;

            default:
                break;
        }

        return result;
    }
};
