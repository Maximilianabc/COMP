// This object represent the postprocessor
Postprocessor = {
    // The postprocess function takes the audio samples data and the post-processing effect name
    // and the post-processing stage as function parameters. It gathers the required post-processing
    // paramters from the <input> elements, and then applies the post-processing effect to the
    // audio samples data of every channels.
    postprocess: function (channels, effect, pass) {
        switch (effect) {
            case "no-pp":
                // Do nothing
                break;

            case "reverse":
                /**
                * TODO: Complete this function
                **/

                // Post-process every channels
                for (var c = 0; c < channels.length; ++c) {
                    // Get the sample data of the channel
                    var audioSequence = channels[c].audioSequenceReference;
                    // Apply the post-processing, i.e. reverse
                    audioSequence.data.reverse();
                    // Update the sample data with the post-processed data
                    channels[c].setAudioSequence(audioSequence);
                }
                break;

            case "boost":
                // Find the maximum gain of all channels
                var maxGain = -1.0;
                for (var c = 0; c < channels.length; ++c) {
                    // Get the sample data of the channel
                    var audioSequence = channels[c].audioSequenceReference;
                    var gain = audioSequence.getGain();
                    if (gain > maxGain) {
                        maxGain = gain;
                    }
                }

                // Determin the boost multiplier
                var multiplier = 1.0 / maxGain;

                // Post-process every channels
                for (var c = 0; c < channels.length; ++c) {
                    // Get the sample data of the channel
                    var audioSequence = channels[c].audioSequenceReference;

                    // For every sample, apply a boost multiplier
                    for (var i = 0; i < audioSequence.data.length; ++i) {
                        audioSequence.data[i] *= multiplier;
                    }

                    // Update the sample data with the post-processed data
                    channels[c].setAudioSequence(audioSequence);
                }
                break;

            case "adsr":
                /**
                * TODO: Complete this function
                **/

                // Obtain all the required parameters
                var attackDuration = parseFloat($("#adsr-attack-duration").data("p" + pass)) * sampleRate;
                var holdDuration = parseFloat($("#adsr-hold-duration").data("p" + pass)) * sampleRate;
                var decayDuration = parseFloat($("#adsr-decay-duration").data("p" + pass)) * sampleRate;
                var releaseDuration = parseFloat($("#adsr-release-duration").data("p" + pass)) * sampleRate;
                var sustainLevel = parseFloat($("#adsr-sustain-level").data("p" + pass)) / 100.0;
                var startPoint = parseFloat($("#adsr-start-point").data("p" + pass)) * sampleRate;
                var endPoint = parseFloat($("#adsr-end-point").data("p" + pass)) * sampleRate;
                var expo = $("#adsr-exponetial").prop("checked");
                var atk_expo = parseFloat($("#adsr-attack-slider").val());
                var decay_release_expo = parseFloat($("#adsr-decay-release-slider").val());

                var labelSequence = [];
                var audioGraphSequence = [];
                var ii = 0;
                var xx = 20;

                for (var c = 0; c < channels.length; ++c) {
                    // Get the sample data of the channel
                    var audioSequence = channels[c].audioSequenceReference;

                    // change the value of xx according to the number of sample to increase process speed
                    xx = ((endPoint - startPoint) / (sampleRate)) * 10;

                    for (var i = 0; i < audioSequence.data.length; ++i) {
                        // TODO: Complete the ADSR postprocessor
                        // Hinst: You can use the function lerp() in utility.js
                        // for performing linear interpolation
                        if (i >= startPoint && i < endPoint) {
                            if (i < startPoint + attackDuration) {
                                var attackPercent = (i + 1) / attackDuration;
                                if (expo) {
                                    audioSequence.data[i] *= lerpExpo(0.0, 1.0, attackPercent, atk_expo);
                                }else {
                                    audioSequence.data[i] *= lerp(0.0, 1.0, attackPercent);
                                }
                            }
                            else if (i < startPoint + attackDuration + holdDuration) {
                                audioSequence.data[i] *= 1;
                            }
                            else if (i < startPoint + attackDuration + holdDuration + decayDuration) {
                                var decayPercent = (i - startPoint - attackDuration - holdDuration + 1) / decayDuration;
                                if (expo) {
                                    audioSequence.data[i] *= lerpExpo(1.0, sustainLevel, decayPercent, decay_release_expo);
                                }else {
                                    audioSequence.data[i] *= lerp(1.0, sustainLevel, decayPercent);
                                }
                            }
                            else if (i < endPoint - releaseDuration) {
                                audioSequence.data[i] *= sustainLevel;
                            }
                            else {
                                var releasePercent = (i - (endPoint - releaseDuration) + 1) / releaseDuration;
                                if (expo) {
                                    audioSequence.data[i] *= lerpExpo(sustainLevel, 0.0, releasePercent, decay_release_expo);
                                }else {
                                    audioSequence.data[i] *= lerp(sustainLevel, 0.0, releasePercent);
                                }
                            }
                            
                            if ( i % xx == 0 ) {
                            audioGraphSequence[ii] = audioSequence.data[i];
                            ii = ii + 1;
                            }
                        }

                    }

                    for (var b = 0; b < audioGraphSequence.length; ++b) {
                        labelSequence[b] = '';
                    }

                    if (expo) {
                        var attackChange = $("#adsr-attack-slider").val();
                        var decayReleaseChange = $("#adsr-decay-release-slider").val();
                    }

                    // Update the sample data with the post-processed data
                    channels[c].setAudioSequence(audioSequence);

                    // start of draw non-editable chart
                    var imported = document.createElement('script');
                    imported.src = 'https://cdnjs.cloudflare.com/ajax/libs/Chart.js/3.7.1/chart.min.js';
                    document.head.appendChild(imported);

                    $("canvas#myChart").remove();
                    $("div#AHDSRGraph").append('<canvas id="myChart"></canvas>');

                    let myChart = document.getElementById('myChart').getContext('2d');

                    let AHDSRChart = new Chart(myChart, {
                        type: 'line',
                        data: {
                            labels: labelSequence,
                            datasets: [{
                                label: 'Left [AHDSR Envelope Graph] Right',
                                data: audioGraphSequence,
                                backgroundColor: 'red'
                            }]
                        },
                        options: {
                            plugins: {
                                legend: {
                                    display:true,
                                    labels: {color: 'black', font: {size: 20}
                                    }
                                }
                            },
                            layout: {padding: 50}
                        }
                    });
                    // End of draw non-editable chart
                }
                break;

            case "tremolo":
                /**
                * TODO: Complete this function
                **/

                // Obtain all the required parameters
                var tremoloFrequency = parseFloat($("#tremolo-frequency").data("p" + pass));
                var wetness = parseFloat($("#tremolo-wetness").data("p" + pass));

                // Post-process every channels
                for (var c = 0; c < channels.length; ++c) {
                    // Get the sample data of the channel
                    var audioSequence = channels[c].audioSequenceReference;
                    // For every sample, apply a tremolo multiplier
                    for (var i = 0; i < audioSequence.data.length; ++i) {
                        var t = i / sampleRate;
                        multiplier = (Math.sin(2 * Math.PI * tremoloFrequency * t - 90) + 1) / 2;
                        multiplier = multiplier * wetness + (1 - wetness);
                        audioSequence.data[i] *= multiplier;
                    }
                    // Update the sample data with the post-processed data
                    channels[c].setAudioSequence(audioSequence);
                }
                break;

            case "echo":
                /**
                * TODO: Complete this function
                **/

                // Obtain all the required parameters
                var delayLineDuration = parseFloat($("#echo-delay-line-duration").data("p" + pass));
                var multiplier = parseFloat($("#echo-multiplier").data("p" + pass));

                // Post-process every channels
                for (var c = 0; c < channels.length; ++c) {
                    // Get the sample data of the channel
                    var audioSequence = channels[c].audioSequenceReference;
                    // Create a new empty delay line
                    var delayLineSize = parseInt(delayLineDuration * sampleRate);
                    var delayLine = [];
                    for (var i = 0; i < delayLineSize; i++)
                        delayLine.push(0);
                    var delayLineOutput;
                    // Get the sample data of the channel
                    for (var i = 0; i < audioSequence.data.length; ++i) {
                        // Get the echoed sample from the delay line
                        delayLineOutput = delayLine[i % delayLineSize];
                        // Add the echoed sample to the current sample, with a multiplier
                        audioSequence.data[i] = audioSequence.data[i] + delayLineOutput * multiplier;
                        // Put the current sample into the delay line
                        delayLine[i % delayLineSize] = audioSequence.data[i];
                    }

                    // Update the sample data with the post-processed data
                    channels[c].setAudioSequence(audioSequence);
                }
                break;

            default:
                // Do nothing
                break;
        }
        return;
    }
}
